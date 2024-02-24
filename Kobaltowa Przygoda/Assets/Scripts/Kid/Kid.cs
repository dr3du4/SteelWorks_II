using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Kid : MonoBehaviour {
    private NavMeshAgent agent;
    [SerializeField] private Transform player = null;
    private Rigidbody2D rb;
    public bool isFollow = false;
    public float maxDistance = 3f;
    public float movementSpeed = 4f;
    private Vector3 _direction;


    static int playerCount;
    public int playerId;

    
    public float efficiency;
    public float hungry;
    public float speed;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        playerId = playerCount;
        playerCount++;
    }
    
    private void Update() {
        if (isFollow) {
            if (Vector3.Distance(player.position, transform.position) > maxDistance) {
                agent.SetDestination(player.position);
            }
        } 
    }
    
    public void StartFollowing() {
        isFollow = true;
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    public void StopFollowing(Vector2 target) {
        isFollow = false;
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
}
