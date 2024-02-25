using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    public float dayLength = 180f;
    public int depositCount = 20;
    public int childCount = 10;
    public Transform cobaltSpawnSpotsParent;
    [AssetsOnly] public GameObject cobaltDepositPrefab;
    public float deliverRange = 3.2f;

    [Title("Debug")]
    [SerializeField] [ReadOnly] float dayTimer = 0f;
    [SerializeField] [ReadOnly] int dayCounter = 0;

    private KidsMaster kidsMaster;

	public UnityEvent OnEndDay;

    // Przepinanie obiektu "cobaltSpawnSpots" przy zmianie mapy
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {	
		OnEndDay.AddListener(EndDayTutorial);
        TutorialSystem.Instance.DisplayTutorial(0);
    }

    private void Update()
    {
        if (!kidsMaster)
            kidsMaster = FindAnyObjectByType<KidsMaster>();

        if (Input.GetKeyDown(KeyCode.L))
            StartDay();

        dayTimer += Time.deltaTime;

        if(dayTimer >= dayLength)
        {
            Debug.Log("END OF DAY");
			OnEndDay?.Invoke();
        }
    }

    void StartDay()
    {
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
