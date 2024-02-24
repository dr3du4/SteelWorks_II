using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Transform _transform;

    [SerializeField] private Vector3 offset = new Vector3(0f,0f,-10f);
    [SerializeField] private float minX = 9.6f; 
    [SerializeField] private float maxX = 62.5f; 
    [SerializeField] private float minY = -48.61f; 
    [SerializeField] private float maxY = -29.39f;
    private Vector3 _newPos;
    
    private void Start() {
        _transform = GetComponent<Transform>();
    }

    private void Update() {
        _newPos = player.position;
        if (player.position.x < minX) _newPos.x = minX;
        else if (player.position.x > maxX) _newPos.x = maxX;
        if (player.position.y < minY) _newPos.y = minY;
        else if (player.position.y > maxY) _newPos.y = maxY;
        _newPos += offset;
        _transform.position = _newPos;
    }
}
