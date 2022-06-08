using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BattleArenaController : MonoBehaviour
{
    [SerializeField] PlayerBehaviour _playerBehaviour;
    [SerializeField] EnemyBehaviour _enemyBehaviour;

    private void OnEnable()
    {
        EventManager.OnCheckWhoScored += CheckWhoScored;
    }
    private void OnDisable()
    {
        EventManager.OnCheckWhoScored += CheckWhoScored;
    }
    public void CheckWhoScored()
    {
        if (_playerBehaviour != null && _enemyBehaviour != null)
        {
            if (_playerBehaviour.currentPlayerMonster.GetComponent<MonsterBehaviour>().Power < _enemyBehaviour.currentEnemyMonster.GetComponent<MonsterBehaviour>().Power)
            {
                //enemy win

                VFXManager.instance.DustVFX(_playerBehaviour.currentPlayerMonster.transform);

                Destroy(_playerBehaviour.currentPlayerMonster.gameObject);
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    _enemyBehaviour._monsters.Remove(_enemyBehaviour._monsters[_enemyBehaviour.rnd]);
                    VFXManager.instance.DustVFX(_enemyBehaviour.currentEnemyMonster.transform);
                    Destroy(_enemyBehaviour.currentEnemyMonster.gameObject);
                    UIManager.instance.SetScoreText(true, false, _enemyBehaviour);

                    if (StateEnums.currentGameState == StateEnums.GameStateEnums.Finish) return;

                    _enemyBehaviour.SpawnPokeballAndTriggerAnim();

                });
            }
            else if (_enemyBehaviour.currentEnemyMonster.GetComponent<MonsterBehaviour>().Power < _playerBehaviour.currentPlayerMonster.GetComponent<MonsterBehaviour>().Power)
            {
                //player win


                _enemyBehaviour._monsters.Remove(_enemyBehaviour._monsters[_enemyBehaviour.rnd]);

                VFXManager.instance.DustVFX(_enemyBehaviour.currentEnemyMonster.transform);
                Destroy(_enemyBehaviour.currentEnemyMonster.gameObject);

                DOVirtual.DelayedCall(0.5f, () =>
                {
                    VFXManager.instance.DustVFX(_playerBehaviour.currentPlayerMonster.transform);
                    Destroy(_playerBehaviour.currentPlayerMonster.gameObject);
                    UIManager.instance.SetScoreText(false, true, _enemyBehaviour);

                    if (StateEnums.currentGameState == StateEnums.GameStateEnums.Finish) return;
                    _enemyBehaviour.SpawnPokeballAndTriggerAnim();
                });


            }
            else
            {
                //both win

                _enemyBehaviour._monsters.Remove(_enemyBehaviour._monsters[_enemyBehaviour.rnd]);

                VFXManager.instance.DustVFX(_playerBehaviour.currentPlayerMonster.transform);
                Destroy(_playerBehaviour.currentPlayerMonster.gameObject);
                VFXManager.instance.DustVFX(_enemyBehaviour.currentEnemyMonster.transform);
                Destroy(_enemyBehaviour.currentEnemyMonster.gameObject);
                UIManager.instance.SetScoreText(true, true, _enemyBehaviour);

                if (StateEnums.currentGameState == StateEnums.GameStateEnums.Finish) return;
                _enemyBehaviour.SpawnPokeballAndTriggerAnim();
            }

        }
    }
}
