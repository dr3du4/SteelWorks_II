using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[RequireComponent(typeof(PlayerMine))]
public class KidsMaster : SerializedMonoBehaviour
{
    [SerializeField] float kidRange = 1.5f;
    [AssetsOnly] [SerializeField] Transform kidPrefab;

    [Title("Binds")]
    [SerializeField] private KeyCode leaveKidBind = KeyCode.Q;
    [SerializeField] private KeyCode gatherKidBind = KeyCode.E;

    [SerializeField] private List<Kid> followingKids;
    private List<Kid> allKids = new();
	private List<Kid> kidsInRange;

    bool refreshKids = false;
    PlayerMine playerMine;

    private void Start()
    {
        allKids = FetchAllKids();
        playerMine = GetComponent<PlayerMine>();
    }
    public List<Kid> KidsList
    {
        get { return allKids; }
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            ReturnKids(2);
        if (refreshKids)
            allKids = FetchAllKids();

        playerMine.minerCount = followingKids.Count;


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
				followingKids.Add(k);
				k.StartFollowing();		
			}
			kidsInRange.Clear();
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
            for (int i = 0; i < count; i++)
            {
                Kid k = followingKids[followingKids.Count - 1];
                ret.Add(k);
                DestroyChild(k);
            }
        }
        return ret;
    }

    public void ReturnKids(List<Kid> returnedKids)
    {
        foreach(Kid k in returnedKids)
        {
            Kid newKid = Instantiate(kidPrefab, transform.position + new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f)), transform.rotation).GetComponent<Kid>();

            newKid.CopyDataFromKid(k);

            allKids.Add(newKid);
            followingKids.Add(newKid);
            newKid.StartFollowing();
        }
    }

    public void ReturnKids(int returnedKids)
    {
        for(int i=0; i<returnedKids; i++)
        {
            Kid k = Instantiate(kidPrefab, transform.position + new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f)), transform.rotation).GetComponent<Kid>();
            allKids.Add(k);
            followingKids.Add(k);
            k.StartFollowing();
        }
    }

    public Kid DestroyChild(Kid k)  
    {
        allKids.Remove(k);
        followingKids.Remove(k);
        Kid ret = k;
        Destroy(k.gameObject);
        return ret;
    }

    public Kid GetRandomKid()
    {
        if(allKids.Count > 0)
            return allKids[Random.Range(0, allKids.Count)];

        foreach(DepositController deposit in FindObjectsOfType<DepositController>())
        {
            if(deposit.GetWorkerCount() > 0)
                return deposit.GetRandomWorker();
        }
        return null;
    }

    public Kid FindKidById(int id)
    {
        return allKids.Find(a => a.playerId == id);
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
