using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;

public class EnemyBehaviour : MonoBehaviour
{
    private Animator _enemyAnim;
    [SerializeField] private GameObject _enemyRightHandRig;
    public List<GameObject> _monsters;
    [SerializeField] private GameObject _littlePokeball;
    [SerializeField] private Transform _pokeballTarget;
    [HideInInspector] public EnemyAnimationController enemyAnimationController;
    public GameObject currentEnemyMonster;
    public int rnd;
    private GameObject _pokeBall;



    private void OnEnable()
    {
        EventManager.OnEnemyThrowPokeball += ThrowPokeball;
        EventManager.OnLevelFinish += LevelFinish;

    }
    private void OnDisable()
    {
        EventManager.OnEnemyThrowPokeball -= ThrowPokeball;
        EventManager.OnLevelFinish -= LevelFinish;
    }


    private void Awake()
    {
        _enemyAnim = GetComponent<Animator>();
        enemyAnimationController = GetComponent<EnemyAnimationController>();
    }



    public void SpawnPokeballAndTriggerAnim() // trigger throw anim and spawn pokeball
    {
        enemyAnimationController.Throw(true);
        _pokeBall = Instantiate(_littlePokeball, _enemyRightHandRig.transform.position, Quaternion.identity, _enemyRightHandRig.transform);

    }

    private void ThrowPokeball() //throw pokeball
    {
        _pokeBall.transform.SetParent(null);
        _pokeBall.transform.DOJump(_pokeballTarget.position, 3, 1, .5f).OnComplete(() =>
        {
            Destroy(_pokeBall);
            rnd = Random.Range(0, _monsters.Count);
            currentEnemyMonster = Instantiate(_monsters[rnd], _pokeballTarget.position, Quaternion.identity);
            MonsterBehaviour monsterBehaviour = currentEnemyMonster.GetComponent<MonsterBehaviour>();
            if (monsterBehaviour.rigModel != null) monsterBehaviour.rigModel.transform.localRotation = Quaternion.Euler(new Vector3(0, -180, 0));
            UIManager.instance.SelectionUICanvas(true);
            EventManager.StartBattle();
            enemyAnimationController.Throw(false);
        });
    }


    private void LevelFinish() // when level finished.
    {
        enemyAnimationController.Throw(false);
    }




}
