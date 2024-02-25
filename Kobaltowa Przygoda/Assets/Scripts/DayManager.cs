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
    public Transform cobaltSpawnSpotsParent;
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
    }

    void CreateRandomDeposits(int count)
    {
        List<Transform> spawnPoints = new(cobaltSpawnSpotsParent.GetComponentsInChildren<Transform>());
        for(int i=0; i<count; i++)
        {
            int j = Random.Range(0, spawnPoints.Count);
            Transform nextSpawn = spawnPoints[j];

            Instantiate(cobaltDepositPrefab, nextSpawn);
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

	
	private void EndDayTutorial()
	{
	    TutorialSystem.Instance.DisplayTutorial(8);
		OnEndDay.RemoveListener(EndDayTutorial);
	}
}
