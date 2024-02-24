using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected NavMeshAgent agent;
    public float activateTimer = 15f;
    public float detectionRange = 10f;

    protected List<GameObject> workers = new();
    protected List<DepositController> cobaltDeposits = new();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        activateTimer += Time.time;

        foreach(GameObject depositObject in GameObject.FindGameObjectsWithTag("CobaltDeposit"))
        {
            cobaltDeposits.Add(depositObject.GetComponent<DepositController>());
        }
    }

    private void Update()
    {
        if (Time.time > activateTimer)
            BeginBehaviour();

        FetchAllWorkers();
    }

    protected abstract void BeginBehaviour();


    protected virtual void FetchAllWorkers()
    {
        workers = new(GameObject.FindGameObjectsWithTag("Kid"));
    }

    protected GameObject FindClosestWorker()
    {
        Vector2 closestDistance = new Vector2(100000000, 100000000);
        GameObject closestWorker = null;
        foreach(GameObject worker in workers)
        {
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
        if(Vector2.Distance(transform.position, closestDistance) <= detectionRange)
            return closestDeposit;
        return null;
    }
}
