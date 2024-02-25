using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.AI;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    public float dayLength = 180f;
    public float priceChangeTime = 15f;
    public int depositCount = 20;
    public int childCount = 10;


    [Space(5)]
    [Title("Map setup")]
    [SerializeField] [SceneObjectsOnly] GameObject map1;
    [SerializeField] [SceneObjectsOnly] GameObject map2;

    [SerializeField] [SceneObjectsOnly] Transform cobaltSpawnSpotsParent1;
    [SerializeField] [SceneObjectsOnly] Transform cobaltSpawnSpotsParent2;


    Transform cobaltSpawnSpotsParent;


    [AssetsOnly] public GameObject cobaltDepositPrefab;
    public float deliverRange = 3.2f;

    List<Kid> safeWorkers = new();

    [Title("Debug")]
    [SerializeField] [ReadOnly] float dayTimer = 0f;
    [SerializeField] [ReadOnly] int dayCounter = 0;

    private KidsMaster kidsMaster;
    private hungryManager hungerManager;

	public UnityEvent OnEndDay;

    // Przepinanie obiektu "cobaltSpawnSpots" przy zmianie mapy
    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {	
		OnEndDay.AddListener(EndDayTutorial);
        TutorialSystem.Instance.DisplayTutorial(0);
        kidsMaster = FindAnyObjectByType<KidsMaster>();
        hungerManager = GetComponent<hungryManager>();
        cobaltSpawnSpotsParent = cobaltSpawnSpotsParent1;
        StartDay(new(), true);
    }

    private void Update()
    {
        if (!kidsMaster)
            kidsMaster = FindAnyObjectByType<KidsMaster>();

        if (Input.GetKeyDown(KeyCode.L))
            StartDay(TallySafeWorkers(), true);

        dayTimer += Time.deltaTime;

        if(dayTimer >= dayLength)
        {
			OnEndDay?.Invoke();
            safeWorkers = TallySafeWorkers();
            StartDay(safeWorkers, false);
        }
    }

    void StartDay(List<Kid> safeWorkers, bool firstDay)
    {
        if (!firstDay)
        {
            if (map1.activeSelf)
            {
                map1.SetActive(false);
                map2.SetActive(true);
                cobaltSpawnSpotsParent = cobaltSpawnSpotsParent2;
            }
            else
            {
                map2.SetActive(false);
                map1.SetActive(true);
                cobaltSpawnSpotsParent = cobaltSpawnSpotsParent1;
            }
        }


        List<Kid> unsafeWorkers = new();
        if(safeWorkers.Count > 0)
        {
            foreach(Kid k in kidsMaster.GetAllKids())
            {
                if (!safeWorkers.Contains(k))
                    unsafeWorkers.Add(k);
            }
        }

        foreach (Kid k in unsafeWorkers)
            kidsMaster.DestroyChild(k);

        WarpWorkersToSpawn(safeWorkers);
        if(kidsMaster)
            kidsMaster.gameObject.transform.position = transform.position;

        if (firstDay)
            hungerManager.eat(20);


        kidsMaster.SpawnKids(childCount);
        dayCounter++;
        dayTimer = 0;

        CreateRandomDeposits(depositCount);

        foreach(Enemy enemy in FindObjectsOfType<Enemy>())
        {
            enemy.NewDay();
        }
    }

    void CreateRandomDeposits(int count)
    {
        foreach(DepositController deposit in FindObjectsOfType<DepositController>())
        {
            Destroy(deposit.gameObject);
        }

        List<Transform> spawnPoints = new(cobaltSpawnSpotsParent.GetComponentsInChildren<Transform>());
        for(int i=0; i<count; i++)
        {
            int j = Random.Range(0, spawnPoints.Count);
            Transform nextSpawn = spawnPoints[j];
            Debug.Log(Instantiate(cobaltDepositPrefab, nextSpawn).name);
            spawnPoints.RemoveAt(j);

        }
        
    }

    private List<Kid> TallySafeWorkers()
    {
        List<Kid> retList = new();

        foreach(Kid k in kidsMaster.GetFollowingKids())
            retList.Add(k);

        foreach(Kid k in kidsMaster.GetAllKids())
        {
            if(!retList.Contains(k) && Vector2.Distance(transform.position, k.transform.position) <= deliverRange)
            {
                retList.Add(k);
            }
        }

        return retList;
    }

    private void WarpWorkersToSpawn(List<Kid> workers)
    {
        foreach(Kid k in workers)
        {
            NavMeshAgent agent = k.GetComponent<NavMeshAgent>();
            agent.Warp(transform.position + new Vector3(Random.Range(-deliverRange, 0), Random.Range(-deliverRange, 0)));
        }
    }

    public float GetTime()
    {
        return  dayTimer;
    }

    public int GetDay()
    {
        return dayCounter;
    }

	
	private void EndDayTutorial()
	{
	    TutorialSystem.Instance.DisplayTutorial(8);
		OnEndDay.RemoveListener(EndDayTutorial);
	}
}
