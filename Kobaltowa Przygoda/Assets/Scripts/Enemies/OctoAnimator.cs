using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoAnimator : MonoBehaviour
{
    private Vector3 previousPosition;

    [SerializeField] private SpriteRenderer _renderer;

    private void Start()
    {
        // Inicjalizacja poprzedniej pozycji
        previousPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckMovementDirection();
    }
    
    private void CheckMovementDirection()
    {
        // Aktualna pozycja obiektu
        Vector3 currentPosition = transform.position;

        // Różnica między aktualną a poprzednią pozycją
        Vector3 movement = currentPosition - previousPosition;

        // Sprawdzenie kierunku na podstawie składowej x
        if (movement.x > 0.01f) {
            _renderer.flipX = true;
        }
        else if (movement.x < -0.01f)
        {
            _renderer.flipX = false;
        }

        // Aktualizacja poprzedniej pozycji
        previousPosition = currentPosition;
    }
}
