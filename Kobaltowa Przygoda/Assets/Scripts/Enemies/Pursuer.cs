using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : Enemy
{
    Kid target;
    int targetId;
    DepositController depositToLoot = null;

    bool escaping = false;

    [SerializeField] float speed = 0.75f;
    [SerializeField] List<Transform> startPoints = new();
    Transform nextStartPoint;


    public override List<Kid> RetrieveWorkers()
    {
        List<Kid> retList = new();
        if (target && escaping)
            retList.Add(target);
        return retList;
    }

    private void Update()
    {
        if (agent.speed != speed)
            agent.speed = speed;

        if (!aiOn)
            aiOn = Setup();

        if(stunned && Time.time > stunTimer)
        {
            stunned = false;
        }

        if (aiOn && !stunned)
        {
            if (escaping)
            {
                // Kid was killed
                agent.SetDestination(nextStartPoint.position);

                if (agent.remainingDistance <= 0.1f)
                {
                    escaping = false;
                    target = null;
                    targetId = -1;
                    depositToLoot = null;
                    StunEnemy(2);
                }
            }
            else
            {
                if (!target && !depositToLoot)
                {
                    bool targetPutToWork = false;
                    
                    foreach(DepositController deposit in FindObjectsOfType<DepositController>())
                    {
                        if (deposit.WorkerInDeposit(target))
                        {
                            targetPutToWork = true;
                            depositToLoot = deposit;
                        }
                    }

                    if (!targetPutToWork)
                    {
                        target = kidsMaster.FindKidById(targetId);

                        if (!target)
                        {
                            target = SelectRandomTarget();
                            targetId = target.playerId;
                        }

                    }
                    else if(depositToLoot)
                        agent.SetDestination(depositToLoot.transform.position);

                }
                else if(depositToLoot)
                {
                    agent.SetDestination(depositToLoot.transform.position);

                    if(agent.remainingDistance <= 0.1f)
                    {
                        // KILLL
                        escaping = true;
                        depositToLoot.KidnapWorker(target);
                        target = null;
                        targetId = -1;
                        nextStartPoint = startPoints[Random.Range(0, startPoints.Count)];
                    }


                }
                else
                {
                    depositToLoot = null;
                    agent.SetDestination(target.transform.position);
                    if(Vector2.Distance(transform.position, target.transform.position) <= 1f)
                    {
                        // KILL
                        escaping = true;
                        kidsMaster.DestroyChild(target);
                        target = null;
                        targetId = -1;
                        nextStartPoint = startPoints[Random.Range(0, startPoints.Count)];
                    }
                }
            }
        }
    }

    private Kid SelectRandomTarget()
    {
        return kidsMaster.GetRandomKid();
    }

    


}
