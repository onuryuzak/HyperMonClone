using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System;
using TMPro;
using DG.Tweening;

public class PlayerBehaviour : MonoBehaviour
{
    public List<GameObject> _battleMonsters;
    [SerializeField] private GameObject _playerRightHandRig;
    [SerializeField] private GameObject _littlePokeball;
    [SerializeField] private Transform _pokeballTarget;
    [SerializeField] private Transform _endBattleTargetPos;
    [SerializeField] private GameObject _playerCanvas;
    [Range(0, 1)]
    [SerializeField] private double _setPercentage = 0;
    public TextMeshProUGUI _pokeballText;
    [SerializeField] private int _maxBackwardAllowCount;

    private SplineFollower _splineFollower;
    private PlayerAnimationController _playerAnimationController;
    private int _backwardAllowCount = 0;
    private int _buttonId;
    private GameObject _pokeBall;
    private UIMonsterSelectionController _uIMonsterSelectionController;
    private PlayerMovementController _playerMovementController;


    public List<MonsterBuilder> takenMonsters;
    public List<Transform> spawnPoints;
    public int maxMonsterCapacity;
    [HideInInspector] public int TotalPokeball;
    public GameObject currentPlayerMonster;




    private void OnEnable()
    {
        EventManager.OnPlayerMoveBackward += PlayerBackward;
        EventManager.OnIncreasePokeballCount += IncreasePokeballCount;
        EventManager.OnPlayerThrowPokeball += PlayerThrowPokeball;
        EventManager.OnLevelFinish += LevelFinish;
    }
    private void OnDisable()
    {
        EventManager.OnPlayerMoveBackward -= PlayerBackward;
        EventManager.OnIncreasePokeballCount -= IncreasePokeballCount;
        EventManager.OnPlayerThrowPokeball -= PlayerThrowPokeball;
        EventManager.OnLevelFinish -= LevelFinish;
    }

    private void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        _splineFollower = GetComponent<SplineFollower>();
        _playerAnimationController = GetComponent<PlayerAnimationController>();
        _uIMonsterSelectionController = FindObjectOfType<UIMonsterSelectionController>();
    }


    private void IncreasePokeballCount(int price)//Increase player collectable total pokeball   
    {
        StartCoroutine(IncreasePokeball(price));
    }

    private void PlayerBackward() //go backward when totalPokeball value < gate monster price value
    {
        _playerMovementController._isMovement = false;
        _playerAnimationController.Backward(true);
        _splineFollower.follow = false;
        double _currentPercentage = _splineFollower.GetPercent();
        double percentageDiff = _currentPercentage - _setPercentage;

        DOVirtual.Float((float)_currentPercentage, (float)percentageDiff, 1f, t =>
        {
            _splineFollower.SetPercent(t);
        });
        _backwardAllowCount += 1;
        if (_maxBackwardAllowCount < _backwardAllowCount) EventManager.LevelFail(true);
    }

    public IEnumerator IncreasePokeball(int price)
    {
        int total = TotalPokeball;
        TotalPokeball += price;
        var count = price;

        while (count > 0)
        {
            var diff = Math.Min(3, count);
            count -= diff;
            total += diff;
            _pokeballText.text = total.ToString();
            yield return null;
        }
    }

    public void RunnerFinish() // when runner platform is finished
    {
        _playerCanvas.SetActive(false);
        EventManager.RunnerFinish();
        _splineFollower.follow = false;
        
        Destroy(_splineFollower);
        foreach (MonsterBuilder item in takenMonsters)
        {
            item.gameObject.SetActive(false);

        }

        Vector3 targetPos = new Vector3(_endBattleTargetPos.position.x, transform.position.y, _endBattleTargetPos.position.z);
        transform.DOMove(_endBattleTargetPos.position, 1f).OnComplete(() =>
        {
            _playerAnimationController.Running(false);
            UIManager.instance.LevelEndBattleUI(true);
            DOVirtual.DelayedCall(0.5f, () => FindObjectOfType<EnemyBehaviour>().SpawnPokeballAndTriggerAnim());
        });
    }
    private void PlayerThrowPokeball() // after select player throw pokeball for battle
    {

        _pokeBall.transform.SetParent(null);
        _pokeBall.transform.DOJump(_pokeballTarget.position, 3, 1, .5f).OnComplete(() =>
        {
            foreach (GameObject item in _battleMonsters)
            {
                if (item.GetComponent<MonsterBehaviour>().id == _buttonId)
                {
                    Destroy(_pokeBall);
                    currentPlayerMonster = Instantiate(item, _pokeballTarget.position, Quaternion.identity);
                    _playerAnimationController.Throw(false);
                    DOVirtual.DelayedCall(1f, () => EventManager.CheckWhoScored());
                    return;
                }
            }


        });
    }
    public void WhenClickSelectionButtons(ButtonController buttonController) //selection button clicked
    {
        _uIMonsterSelectionController.SetNewButtonID();
        _buttonId = buttonController.id;
        buttonController.parentTransform.gameObject.SetActive(false);
        SpawnPokeballAndTriggerAnim();
        UIManager.instance.SelectionUICanvas(false);
    }
    private void SpawnPokeballAndTriggerAnim() // spawn pokeball and trigger throw anim.
    {
        _playerAnimationController.Throw(true);
        _pokeBall = Instantiate(_littlePokeball, _playerRightHandRig.transform.position, Quaternion.identity, _playerRightHandRig.transform);

    }


    private void LevelFinish() // when level finished.
    {
        _playerAnimationController.Throw(false);
    }

}
