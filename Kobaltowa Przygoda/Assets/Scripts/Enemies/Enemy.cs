using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected KidsMaster kidsMaster;
    public float activateTimer = 15f;
    public float stunTime = 2.5f;
    public float detectionRange = 10f;
    public float kidnapRange = 1.5f;
    protected bool aiOn = false;

    protected bool refreshWorkerList = false;


    protected List<GameObject> workers = new();
    protected List<DepositController> cobaltDeposits = new();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        kidsMaster = FindObjectOfType<KidsMaster>();
        activateTimer += Time.time;

        foreach(GameObject depositObject in GameObject.FindGameObjectsWithTag("CobaltDeposit"))
        {
            cobaltDeposits.Add(depositObject.GetComponent<DepositController>());
        }
    }



    protected virtual void BeginBehaviour() 
    {
        aiOn = true;
    }
    public abstract int RetrieveWorkers();

    protected bool Setup()
    {
        if (Time.time > activateTimer)
        {
            FetchAllWorkers();
            BeginBehaviour();
            return true;
        }
        return false;
    }

    protected virtual void FetchAllWorkers()
    {
        workers = new(GameObject.FindGameObjectsWithTag("Kid"));
        refreshWorkerList = false;
    }

    protected GameObject FindClosestWorker()
    {
        Vector2 closestDistance = new Vector2(100000000, 100000000);
        GameObject closestWorker = null;
        foreach(GameObject worker in workers)
        {
            if (!worker)
            {
                refreshWorkerList = true;
                continue;
            }

            if (Vector2.Distance(transform.position, worker.transform.position) < Vector2.Distance(transform.position, closestDistance))
            {
                closestDistance = worker.transform.position;
                closestWorker = worker;
            }
        }
        if(Vector2.Distance(transform.position, closestDistance) <= detectionRange)
            return closestWorker;
        return null;
    }

    protected DepositController FindClosestDepositWithWorker()
    {
        Vector2 closestDistance = new Vector2(100000000000000, 10000000000);
        DepositController closestDeposit = null;
        foreach(DepositController deposit in cobaltDeposits)
        {
            if(Vector2.Distance(transform.position, deposit.transform.position) < Vector2.Distance(transform.position, closestDistance))
            {
                closestDistance = deposit.transform.position;
                closestDeposit = deposit;
            }
        }
        if(Vector2.Distance(transform.position, closestDistance) <= detectionRange && closestDeposit.MiningStatus())
            return closestDeposit;
        return null;
    }

    protected (Transform, TargetType) GetTarget()
    {
        GameObject closestWorker = FindClosestWorker();
        DepositController closestDepositWorker = FindClosestDepositWithWorker();

        if (closestWorker && !closestDepositWorker)
            return (closestWorker.transform, TargetType.WORKER);

        if (!closestWorker && closestDepositWorker)
            return (closestDepositWorker.transform, TargetType.DEPOSIT_WORKER);

        if (closestWorker && closestDepositWorker)
        {
            if (Vector2.Distance(transform.position, closestWorker.transform.position) > Vector2.Distance(transform.position, closestDepositWorker.transform.position))
                return (closestDepositWorker.transform, TargetType.DEPOSIT_WORKER);
            else
                return (closestWorker.transform, TargetType.WORKER);
        }
        return (null, TargetType.NONE);
    }

    public enum TargetType
    {
        WORKER,
        DEPOSIT_WORKER,
        NONE
    }
}
