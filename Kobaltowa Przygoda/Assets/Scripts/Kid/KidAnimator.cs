using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Kid _kid;

    private void Start() {
        _kid = GetComponent<Kid>();
    }
    
    private void Update()
    {
        _animator.SetBool("isMining", _kid.isMining);
        _animator.SetBool("isFollowing", _kid.isFollow);
    }
}
