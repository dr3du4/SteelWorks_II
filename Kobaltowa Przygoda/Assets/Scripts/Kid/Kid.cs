using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Kid : MonoBehaviour {
    private NavMeshAgent agent;
    [SerializeField] private Transform player = null;
    public bool isFollow = false;
    public bool isEaten = false;
    public float maxDistance = 3f;
    public float movementSpeed = 4f;
    private Vector3 _direction;

    public bool isMining = false;


    static int playerCount;
    public int playerId;

    public int holdCobalt;
    public float efficiency;
    public float hungry;
    public float speed;
    public float ladownsc;

    public int maxCobalt = 0;
    
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        playerId = playerCount;

        if (ladownsc == 0)
            maxCobalt = 5;

        playerCount++;
    }
    
    private void Update() {
        if(maxCobalt == 0)
            maxCobalt = (int)(5 * ladownsc);

        if (isFollow && !isMining) {
            if (Vector3.Distance(player.position, transform.position) > maxDistance) {
                agent.SetDestination(player.position);
            }
        } 

        if(Vector2.Distance(transform.position, DayManager.Instance.transform.position) <= DayManager.Instance.deliverRange)
            DayManager.Instance.GetComponent<backpack>().DeliverCobalt(this);
    }
    
    public void StartFollowing() {
        if (!isMining)
        {
            isFollow = true;
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }
        }
    }

    public void FollowMonster(Transform monster)
    {
        /*isFollow = true;
        player = monster;
        agent.speed = 10f;*/
        isFollow = true;
        isEaten = true;
        agent.ResetPath();
    }

    public void SaveWorker()
    {
        isEaten = false;
        isFollow = false;
    }

    public void StopFollowing(Vector2 target) {
        isFollow = false;
        player = null;
        agent.SetDestination(target);
    }

    public Transform GetPlayer()
    {
        return player;
    }

    public void CopyDataFromKid(Kid k)
    {
        // Copy data
        player = k.GetPlayer();
        playerId = k.playerId;
    }

    public void BeginMining(Vector3 position)
    {
        isMining = true;
        agent.SetDestination(position);
    }

    public void StopMining()
    {
        isMining = false;
    }
    
}
