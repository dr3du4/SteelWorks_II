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
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        if (isFollow) {
            if (Vector3.Distance(player.position, transform.position) > maxDistance) {
                agent.SetDestination(player.position);
            }
        } else rb.velocity = Vector2.zero;
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
}
