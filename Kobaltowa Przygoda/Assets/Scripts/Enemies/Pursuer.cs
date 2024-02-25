using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : Enemy
{
    public Animator _animator;
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
        if (!TutorialSystem.Instance.tutorialWas(5) && _renderer.isVisible) TutorialSystem.Instance.DisplayTutorial(5);
        
        if (agent.speed != speed)
            agent.speed = speed;

        if (!aiOn)
            aiOn = Setup();

        if(stunned && Time.time > stunTimer)
        {
            stunned = false;
        }

        if (stunned)
            agent.ResetPath();

        if (aiOn && !stunned)
        {
            if (escaping)
            {
                // Kid was killed
                agent.SetDestination(nextStartPoint.position);

                if (agent.remainingDistance <= 0.1f)
                {
                    _animator.ResetTrigger("Attack");
                    escaping = false;
                    target = null;
                    targetId = -1;
                    depositToLoot = null;
                    StunEnemy(2);
                }
            }
            else
            {
                if (!target)
                {
                    target = SelectRandomTarget();
                    if(target != null)
                        targetId = target.playerId;
                }
                else
                {
                    if (target.isMining && !depositToLoot)
                    {
                        // bool targetPutToWork = false;

                        foreach (DepositController deposit in FindObjectsOfType<DepositController>())
                        {
                            if (deposit.WorkerInDeposit(target))
                            {
                                // targetPutToWork = true;
                                depositToLoot = deposit;
                            }
                        }

                        /*
                        if (!targetPutToWork)
                            target = kidsMaster.FindKidById(targetId);*/
                    }
                    else if (target.isMining && depositToLoot)
                    {
                        agent.SetDestination(depositToLoot.transform.position);

                        if (Vector2.Distance(transform.position, target.transform.position) <= 1f)
                        {
                            // KILLL
                            _animator.SetTrigger("Attack");
                            escaping = true;
                            kidsMaster.DestroyChild(depositToLoot.KidnapWorker(target));
                            target = null;
                            targetId = -1;
                            nextStartPoint = startPoints[Random.Range(0, startPoints.Count)];
                        }
                    }
                    else
                    {
                        depositToLoot = null;
                        agent.SetDestination(target.transform.position);
                        if (Vector2.Distance(transform.position, target.transform.position) <= 1f)
                        {
                            // KILL
                            _animator.SetTrigger("Attack");
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
    }

    private Kid SelectRandomTarget()
    {
        return kidsMaster.GetRandomKid();
    }

    


}
