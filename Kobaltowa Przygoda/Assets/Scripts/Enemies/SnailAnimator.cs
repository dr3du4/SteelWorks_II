using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private BaseEnemy _based;
    private Vector3 previousPosition;

    [SerializeField] private SpriteRenderer _renderer;

    private void Start()
    {
        // Inicjalizacja poprzedniej pozycji
        _based = GetComponent<BaseEnemy>();
        previousPosition = transform.position;
    }
    private void Update()
    {
        _animator.SetBool("full", _based.kidnapping);
        CheckMovementDirection();
    }
    
    private void CheckMovementDirection()
    {
        // Aktualna pozycja obiektu
        Vector3 currentPosition = transform.position;

        // Różnica między aktualną a poprzednią pozycją
        Vector3 movement = currentPosition - previousPosition;

        // Sprawdzenie kierunku na podstawie składowej x
        if (movement.x > 0) {
            _renderer.flipX = true;
        }
        else if (movement.x < 0)
        {
            _renderer.flipX = false;
        }

        // Aktualizacja poprzedniej pozycji
        previousPosition = currentPosition;
    }
}

