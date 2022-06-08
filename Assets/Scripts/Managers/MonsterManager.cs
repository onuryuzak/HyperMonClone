using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterManager : MonoBehaviour
{
    private MonsterBuilder _monsterBuilderPrefab;
    public int spawnPointIndex = 0;

    private void OnEnable()
    {
        EventManager.OnSpawnMonster += MonsterSpawn;
    }
    private void OnDisable()
    {
        EventManager.OnSpawnMonster -= MonsterSpawn;
    }
    private void Awake()
    {
        _monsterBuilderPrefab = (Resources.Load("Builder/MonsterBuilder") as GameObject).GetComponent<MonsterBuilder>();

    }

    private void MonsterSpawn(MonsterType type, PlayerBehaviour _playerBehaviour, int price)
    {
        if (_playerBehaviour.takenMonsters.Count < _playerBehaviour.maxMonsterCapacity)
        {
            _playerBehaviour.TotalPokeball -= price;
            _playerBehaviour._pokeballText.text = "" + _playerBehaviour.TotalPokeball;
            MonsterBuilder monsterBuilder = Instantiate(_monsterBuilderPrefab, _playerBehaviour.transform.position, _monsterBuilderPrefab.transform.rotation, _playerBehaviour.spawnPoints[spawnPointIndex].transform);
            monsterBuilder.SetEnemyType(type, _playerBehaviour);
            monsterBuilder.AddYourselfToMonsterList(_playerBehaviour);
            if (2 == spawnPointIndex) EventManager.ChangeCameraFOV();
            spawnPointIndex += 1;
        }
    }
}
