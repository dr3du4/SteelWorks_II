using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : Enemy
{
    [SerializeField] private float pursuitEndCooldown = 7f;
    private float pursuitEndedTimer = 0f;

    [SerializeField] float normalSpeed = 5f;
    [SerializeField] float kidnapSpeed = 2.5f;

    public List<Transform> path;
    public Transform nest;
    private int currentGoalIndex;


    private Transform target;
    private TargetType currentTargetType;

    
    bool chasing = false;
    public bool kidnapping = false;
    List<Kid> carriedWorkers = new();

    private void Update()
    {
        if (!TutorialSystem.Instance.tutorialWas(4) && _renderer.isVisible) TutorialSystem.Instance.DisplayTutorial(4);
        
        if (refreshWorkerList)
            FetchAllWorkers();

        if (!aiOn)
            chasing = Setup();

        if (stunned && Time.time > stunTimer)
        {
            stunned = false;
            IgnoreTargets(pursuitEndCooldown);
        }

        if (stunned)
            agent.ResetPath();

        if (aiOn && !stunned)
        {
            if (chasing && target && !kidnapping)
            {
                if (agent.speed != normalSpeed)
                    agent.speed = normalSpeed;

                // Pursuit
                agent.SetDestination(target.position);
                float curDist = Vector2.Distance(transform.position, target.position);
                if (curDist > detectionRange)
                {
                    // Target was lost
                    pursuitEndedTimer = Time.time + pursuitEndCooldown;
                    chasing = false;
                }
                else if (curDist <= kidnapRange)
                {
                    // Kidnap worker
                    kidnapping = true;
                    if (currentTargetType == TargetType.DEPOSIT_WORKER)
                        carriedWorkers.Add(target.GetComponent<DepositController>().KidnapWorker());
                    else if (currentTargetType == TargetType.WORKER)
                    {
                        Kid k = target.gameObject.GetComponent<Kid>();
                        carriedWorkers.Add(k);
                        k.StopMining();
                        k.FollowMonster(transform);
                        //kidsMaster.DestroyChild(target.gameObject.GetComponent<Kid>());
                    }
                }
            }
            else if (kidnapping)
            {
                agent.SetDestination(nest.position);
                if(agent.speed != kidnapSpeed)
                    agent.speed = kidnapSpeed;

                foreach(Kid k in carriedWorkers)
                {
                    if (k == null) continue;
                    k.transform.position = transform.position + new Vector3(0, 1f);
                    //k.transform.SetPositionAndRotation(transform.position + new Vector3(0, 1f), Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z));
                    //k.transform.SetPositionAndRotation(transform.position + new Vector3(0, 0.5f), Quaternion.Euler(0, 0, 180f));
                    //k.transform.rotation.SetEulerAngles();
                    k.GetComponent<NavMeshAgent>().ResetPath();
                    k.GetComponent<NavMeshAgent>().height = 0.1f;
                }

                if (agent.remainingDistance <= 0.1f)
                {
                    // kidnapping done
                    Debug.Log("kidnapping done");
                    IgnoreTargets(pursuitEndCooldown);
                    foreach (Kid k in carriedWorkers)
                        kidsMaster.DestroyChild(k);
                    carriedWorkers.Clear();
                }
            }
            else
            {
                if (agent.speed != normalSpeed)
                    agent.speed = normalSpeed;


                if (agent.remainingDistance <= 0.1f)
                {
                    agent.SetDestination(path[currentGoalIndex].position);
                    currentGoalIndex++;
                    if (currentGoalIndex >= path.Count)
                        currentGoalIndex = 0;
                }

                (target, currentTargetType) = GetTarget();

                if (target && Time.time >= pursuitEndedTimer)
                {
                    chasing = true;
                }
            }
        }
    }


    public override List<Kid> RetrieveWorkers()
    {
        List<Kid> retVal = carriedWorkers;
        carriedWorkers = new();
        Debug.Log(retVal.Count);
        IgnoreTargets(pursuitEndCooldown);
        return new List<Kid>(carriedWorkers);
    }

    public void SaveKid()
    {
        if(carriedWorkers.Count > 0)
        {
            foreach(Kid k in carriedWorkers)
            {
                k.SaveWorker();
                k.StartFollowing();
            }

            carriedWorkers.Clear();
        }
    }

    private void IgnoreTargets(float time)
    {
        kidnapping = false;
        pursuitEndedTimer = Time.time + time;
        chasing = true;
    }

    public List<Kid> GetCarriedWorkers()
    {
        return carriedWorkers;
    }
}
