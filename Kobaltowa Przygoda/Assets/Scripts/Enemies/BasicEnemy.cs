using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public List<Transform> path;
    private Transform target;
    private int currentGoalIndex;
    bool patrolling = false;

    protected override void BeginBehaviour()
    {
        patrolling = true;
    }

    private void Update()
    {
        if (patrolling)
        {
            if(agent.isStopped)
            {
                agent.SetDestination(path[currentGoalIndex].position);
                currentGoalIndex++;
                if (currentGoalIndex >= path.Count)
                    currentGoalIndex = 0;
            }

        }
    }

    
}
