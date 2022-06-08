using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointFollow : MonoBehaviour
{
    private PlayerBehaviour _playerBehaviour;
    private Vector3 _targetPos;
    private Vector3 _initPos;
    void Awake()
    {
        _playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        _initPos = transform.position;
    }
    private void Update()
    {
        _targetPos = new Vector3(_playerBehaviour.transform.position.x, 0, _playerBehaviour.transform.position.z);
        _targetPos += _initPos;
        transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * 10f);
    }
}
