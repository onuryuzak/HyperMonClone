using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum MonsterType
{
    Bat,
    Ghost,
    Phantom,
    Spider
}
public class MonsterBuilder : MonoBehaviour
{
    [HideInInspector] public MonsterType _monsterType;
    private GameObject _monster;
    private void Start()
    {
        transform.DOLocalMove(Vector3.zero, 1f);
    }
    public void SetEnemyType(MonsterType type, PlayerBehaviour _playerBehaviour) //Set monster type
    {
        _monsterType = type;
        SpawnMonster();
    }
    public void SpawnMonster()// fetch monster prefab from asset folder
    {
        _monster = Resources.Load("Monster/" + _monsterType) as GameObject;
        Instantiate(_monster, transform.position, _monster.transform.rotation, transform);

    }
    public void AddYourselfToMonsterList(PlayerBehaviour _playerBehaviour)//add taken monster list
    {
        _playerBehaviour.takenMonsters.Add(this);


    }
}
