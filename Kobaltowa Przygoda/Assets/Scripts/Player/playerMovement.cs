using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class playerMovement : MonoBehaviour
{
    public float speed = 5f;

    [HideInInspector] public float playerLookAngle;

    private Animator _animator;
    private Rigidbody2D rb;
    private SpriteRenderer _renderer;
    private Vector3 previousPosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CalculateMovement();
        
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        LookTowards(mousePos);
        CheckMovementDirection();
    }

    void CalculateMovement()
    {
        rb.velocity = Vector3.zero;
        float horizontalInput = Input.GetAxisRaw("Horizontal") * speed;
        float verticalInput = Input.GetAxisRaw("Vertical") * speed;
        rb.velocity = Vector2.ClampMagnitude(new Vector2(horizontalInput, verticalInput), speed);
        if (rb.velocity != Vector2.zero) _animator.SetBool("Running", true);
        else  _animator.SetBool("Running", false);
    }


    private void LookTowards(Vector3 TargetPosition)
    {
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(TargetPosition.y - transform.position.y, TargetPosition.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        //transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        playerLookAngle = AngleDeg;
    }
    
    private void CheckMovementDirection()
    {
        // Aktualna pozycja obiektu
        Vector3 currentPosition = transform.position;

        // Różnica między aktualną a poprzednią pozycją
        Vector3 movement = currentPosition - previousPosition;

        // Sprawdzenie kierunku na podstawie składowej x
        if (movement.x > 0.1f) {
            _renderer.flipX = true;
        }
        else if (movement.x < -0.1f)
        {
            _renderer.flipX = false;
        }

        // Aktualizacja poprzedniej pozycji
        previousPosition = currentPosition;
    }
}
