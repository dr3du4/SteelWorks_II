using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class playerMovement : MonoBehaviour
{
    public float speed = 5f;

    [HideInInspector] public float playerLookAngle;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CalculateMovement();

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        LookTowards(mousePos);
    }

    void CalculateMovement()
    {
        rb.velocity = Vector3.zero;
        float horizontalInput = Input.GetAxisRaw("Horizontal") * speed;
        float verticalInput = Input.GetAxisRaw("Vertical") * speed;

        rb.velocity = Vector2.ClampMagnitude(new Vector2(horizontalInput, verticalInput), speed);
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
}
