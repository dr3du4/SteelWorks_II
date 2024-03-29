using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[RequireComponent(typeof(PlayerMine))]
public class KidsMaster : SerializedMonoBehaviour
{
    [SerializeField] float kidRange = 1.5f;
    [AssetsOnly] [SerializeField] Transform blackKidPrefab;
    [AssetsOnly] [SerializeField] Transform whiteKidPrefab;
    [AssetsOnly] [SerializeField] Transform yellowKidPrefab;
    [AssetsOnly] [SerializeField] Transform kidPrefab;
    [AssetsOnly] [SerializeField] Transform lootPrefab;
    
    [Title("Binds")]
    [SerializeField] private KeyCode leaveKidBind = KeyCode.Q;
    [SerializeField] private KeyCode gatherKidBind = KeyCode.E;

    [SerializeField] private List<Kid> followingKids;
    private List<Kid> allKids = new();
	private List<Kid> kidsInRange;

    private List<BasicEnemy> allKidnappers = new();
    private BasicEnemy kidnapperInRange;

    bool refreshKids = false;
    PlayerMine playerMine;

    private void Start()
    {
        allKids = FetchAllKids();
        playerMine = GetComponent<PlayerMine>();

        allKidnappers = new(FindObjectsOfType<BasicEnemy>());
    }
    public List<Kid> KidsList
    {
        get { return allKids; }
    }
    private void Update() {
        UpdateClosestKidnapper();

        
        if (refreshKids)
            allKids = FetchAllKids();

        playerMine.minerCount = followingKids.Count;
        playerMine.totalCobalt = 0;
        playerMine.maxHeldCobalt = 0;
        foreach(Kid k in followingKids)
        {
            playerMine.totalCobalt += k.holdCobalt;
            playerMine.maxHeldCobalt += k.maxCobalt;
        }

        kidsInRange = UpdateKidsInRange();

        if (Input.GetKeyDown(leaveKidBind)) {
            if (followingKids.Count > 0) {
                Kid k = followingKids[0];
                followingKids.RemoveAt(0);
                k.StopFollowing(transform.position);
            } 
        }

		if (Input.GetKeyDown(gatherKidBind)) {
            foreach(Kid k in kidsInRange) {
                if (!k.isMining)
                {
                    followingKids.Add(k);
                    k.StartFollowing();
                }
			}
			kidsInRange.Clear();

            if (kidnapperInRange)
            {
                Debug.Log("boop");
                kidnapperInRange.SaveKid();
            }
        }

    }
    
    private List<Kid> FetchAllKids()
    {
        refreshKids = false;
        return new List<Kid>(FindObjectsOfType<Kid>());
    }

    private List<Kid> UpdateKidsInRange()
    {
        List<Kid> retVal = new();
        foreach(Kid kid in allKids)
        {
            if (!kid)
            {
                refreshKids = true;
                continue;
            }

            if (Vector2.Distance(transform.position, kid.transform.position) <= kidRange)
                retVal.Add(kid);
        }
        return retVal;
    }

    public List<Kid> RemoveKids(int count)
    {
        List<Kid> ret = new();
        if(followingKids.Count >= count)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                Kid k = followingKids[i];
                ret.Add(k);
                //DestroyChild(k);
            }
            foreach(Kid k in ret)
            {
                k.StopFollowing(transform.position);
                Debug.Log(followingKids.Remove(k));
            }
        }
        return ret;
    }

    public void ReturnKids(List<Kid> returnedKids)
    {
        foreach(Kid k in returnedKids)
        {
            //allKids.Add(k);
            followingKids.Add(k);
            k.StartFollowing();
        }
    }

    public void SpawnKids(int returnedKids)
    {
        for(int i=0; i<returnedKids; i++)
            SpawnSpecificWorker(Random.Range(1, 4));
    }

    public void SpawnSpecificWorker(int workerType)
    {
        switch (workerType)
        {
            case 1:
                kidPrefab = whiteKidPrefab;
                break;
            case 2:
                kidPrefab = yellowKidPrefab;
                break;
            case 3:
                kidPrefab = blackKidPrefab;
                break;

        }

        Kid k = Instantiate(kidPrefab, transform.position + new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f), 0f), transform.rotation).GetComponent<Kid>();
        allKids.Add(k);
        followingKids.Add(k);
        k.StartFollowing();
    }

    public Kid DestroyChild(Kid k)
    {
        allKids.Remove(k);
        followingKids.Remove(k);
        Kid ret = k;
        Loot l = Instantiate(lootPrefab, ret.transform.position, Quaternion.identity).GetComponent<Loot>();
        l.cobalt = k.holdCobalt;
        Destroy(k.gameObject);
        return ret;
    }

    public Kid GetRandomKid()
    {
        if(allKids.Count > 0)
            return allKids[Random.Range(0, allKids.Count)];
        /*
        foreach(DepositController deposit in FindObjectsOfType<DepositController>())
        {
            if(deposit.GetWorkerCount() > 0)
                return deposit.GetRandomWorker();
        }*/
        return null;
    }

    public Kid FindKidById(int id)
    {
        return allKids.Find(a => a.playerId == id);
    }

    public List<Kid> GetFollowingKids()
    {
        return followingKids;
    }

    public List<Kid> GetAllKids()
    {
        return allKids;
    }

    private void UpdateClosestKidnapper()
    {
        kidnapperInRange = null;
        foreach(BasicEnemy b in allKidnappers)
        {
            if(!kidnapperInRange || Vector2.Distance(transform.position, b.transform.position) < Vector2.Distance(transform.position, kidnapperInRange.transform.position))
            {
                kidnapperInRange = b;
            }
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D other) {
		if (transform.parent != null && other.transform.parent.CompareTag("Kid")){
            Kid k = other.transform.parent.GetComponent<Kid>();
			if(!followingKids.Contains(k) && !kidsInRange.Contains(k)) {
				kidsInRange.Add(k);
			}
        }
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (transform.parent != null && other.transform.parent.CompareTag("Kid")){
            Kid k = other.transform.parent.GetComponent<Kid>();
			kidsInRange.Remove(k);
        }
	}
    */
}
