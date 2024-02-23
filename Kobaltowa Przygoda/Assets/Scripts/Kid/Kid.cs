using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour {
    private Rigidbody2D rb;
    [SerializeField] private Transform player = null;
    public bool isFollow = false;
    public float maxDistance = 3f;
    public float movementSpeed = 4f;
    private Vector3 _direction;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        if (isFollow) {
            if (Vector3.Distance(player.position, transform.position) > maxDistance) {
                _direction = (player.position - transform.position).normalized;
                rb.velocity = Vector2.zero;
                rb.velocity = Vector2.ClampMagnitude(new Vector2(_direction.x,_direction.y) * movementSpeed, movementSpeed);
            } else rb.velocity = Vector3.zero;
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
        StartCoroutine(GoTo(target));
    }

    private IEnumerator GoTo(Vector2 target) {
        while (Vector3.Distance(target, transform.position) > 0.2f) {
            _direction = (target - new Vector2(transform.position.x,transform.position.y)).normalized;
            rb.velocity = Vector2.zero;
            rb.velocity = Vector2.ClampMagnitude(new Vector2(_direction.x,_direction.y) * movementSpeed, movementSpeed);
            yield return null;
        }
        rb.velocity = Vector3.zero;
    }
}
