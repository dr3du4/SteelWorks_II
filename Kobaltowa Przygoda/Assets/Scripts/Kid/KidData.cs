using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidData
{
    public int playerId = -1;

    public Vector3 position;

    public Transform player;
    public float efficiency;
    public float hungry;
    public float speed;

    public KidData(Transform _player, Vector3 _position, int _playerId, float _efficiency, float _hungry, float _speed)
    {
        position = _position;
        player = _player;
        playerId = _playerId;
        efficiency = _efficiency;
        hungry = _hungry;
        speed = _speed;
    }
}
