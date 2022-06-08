using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void OnPlayerMoveBackwardDelegate();
    public static event OnPlayerMoveBackwardDelegate OnPlayerMoveBackward;

    public static void PlayerMoveBackward()
    {
        OnPlayerMoveBackward?.Invoke();
    }

    public delegate void OnSpawnMonsterDelegate(MonsterType type, PlayerBehaviour playerBehaviour, int price);
    public static event OnSpawnMonsterDelegate OnSpawnMonster;

    public static void SpawnMonster(MonsterType type, PlayerBehaviour playerBehaviour, int price)
    {
        OnSpawnMonster?.Invoke(type, playerBehaviour, price);
    }

    public delegate void OnIncreasePokeballCountDelegate(int price);
    public static event OnIncreasePokeballCountDelegate OnIncreasePokeballCount;

    public static void IncreasePokeballCount(int price)
    {
        OnIncreasePokeballCount?.Invoke(price);
    }

    public delegate void OnDecreasePokeballCountDelegate(int price);
    public static event OnDecreasePokeballCountDelegate OnDecreasePokeballCount;

    public static void DecreasePokeballCount(int price)
    {
        OnDecreasePokeballCount?.Invoke(price);
    }

    public delegate void OnLevelFailDelegate(bool state);
    public static event OnLevelFailDelegate OnLevelFail;

    public static void LevelFail(bool state)
    {
        OnLevelFail?.Invoke(state);
    }

    public delegate void OnChangeCameraFOVDelegate();
    public static event OnChangeCameraFOVDelegate OnChangeCameraFOV;

    public static void ChangeCameraFOV()
    {
        OnChangeCameraFOV?.Invoke();
    }

    public delegate void OnRunnerFinishDelegate();
    public static event OnRunnerFinishDelegate OnRunnerFinish;

    public static void RunnerFinish()
    {
        OnRunnerFinish?.Invoke();
    }

    public delegate void OnPlayerThrowPokeballDelegate();
    public static event OnPlayerThrowPokeballDelegate OnPlayerThrowPokeball;

    public static void PlayerThrowPokeball()
    {
        OnPlayerThrowPokeball?.Invoke();
    }

    public delegate void OnEnemyThrowPokeballDelegate();
    public static event OnEnemyThrowPokeballDelegate OnEnemyThrowPokeball;

    public static void EnemyThrowPokeball()
    {
        OnEnemyThrowPokeball?.Invoke();
    }

    public delegate void OnCheckWhoScoredDelegate();
    public static event OnCheckWhoScoredDelegate OnCheckWhoScored;

    public static void CheckWhoScored()
    {
        OnCheckWhoScored?.Invoke();
    }
    public delegate void OnStartBattleDelegate();
    public static event OnStartBattleDelegate OnStartBattle;

    public static void StartBattle()
    {
        OnStartBattle?.Invoke();
    }

    public delegate void OnLevelFinishDelegate();
    public static event OnLevelFinishDelegate OnLevelFinish;

    public static void LevelFinish()
    {
        OnLevelFinish?.Invoke();
    }
}
