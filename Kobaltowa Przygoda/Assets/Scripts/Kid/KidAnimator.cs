using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Kid _kid;
    private Vector3 previousPosition;

    [SerializeField] private SpriteRenderer _renderer;

    private void Start()
    {
        // Inicjalizacja poprzedniej pozycji
        _kid = GetComponent<Kid>();
        previousPosition = transform.position;
    }
    private void Update()
    {
        _animator.SetBool("isMining", _kid.isMining);
        _animator.SetBool("isFollowing", _kid.isFollow);
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
