using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    [SerializeField] private float pursuitEndCooldown = 7f;
    private float pursuitEndedTimer = 0f;
    private float stunTimer = 0f;

    [SerializeField] float normalSpeed = 5f;
    [SerializeField] float kidnapSpeed = 2.5f;

    public List<Transform> path;
    public Transform nest;
    private int currentGoalIndex;


    private Transform target;
    private TargetType currentTargetType;

    bool stunned = false;
    bool chasing = false;
    bool kidnapping = false;
    int carriedWorkers = 0;

    private void Update()
    {
        if (!aiOn)
            chasing = Setup();

        if (stunned && Time.time > stunTimer)
        {
            stunned = false;
            IgnoreTargets(pursuitEndCooldown);
        }

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
                        carriedWorkers += target.GetComponent<DepositController>().KidnapWorker();
                    else if (currentTargetType == TargetType.WORKER)
                    {
                        carriedWorkers += 1;
                        Destroy(target.gameObject);
                    }
                }
            }
            else if (kidnapping)
            {
                agent.SetDestination(nest.position);
                if(agent.speed != kidnapSpeed)
                    agent.speed = kidnapSpeed;

                if (agent.remainingDistance <= 0.1f)
                {
                    // kidnapping done
                    Debug.Log("kidnapping done");
                    IgnoreTargets(pursuitEndCooldown);
                    carriedWorkers = 0;
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

    public override int RetrieveWorkers()
    {
        int retVal = carriedWorkers;
        carriedWorkers = 0;
        IgnoreTargets(pursuitEndCooldown);
        return retVal;
    }

    public void StunEnemy(int stunLevel)
    {
        stunTimer = Time.time + (stunTime * stunLevel);
        stunned = true;
    }

    private void IgnoreTargets(float time)
    {
        kidnapping = false;
        pursuitEndedTimer = Time.time + time;
        chasing = true;
    }
}
