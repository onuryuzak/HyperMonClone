using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _enemyUICanvas;
    [SerializeField] private GameObject _playerUICanvas;
    [SerializeField] private GameObject _failUICanvas;
    [SerializeField] private GameObject _successUICanvas;
    [SerializeField] private CanvasGroup _tapToPlayCanvas;
    public GameObject _selectionUICanvas;

    [SerializeField] private TextMeshProUGUI _enemyScoreText;
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    private int _enemyCurrentScore = 0;
    private int _playerCurrentScore = 0;


    private bool _isEnabled;
    private void OnEnable()
    {
        EventManager.OnLevelFail += LevelFail;
    }
    private void OnDisable()
    {
        EventManager.OnLevelFail -= LevelFail;
    }

    void Start()
    {
        StateEnums.currentGameState = StateEnums.GameStateEnums.Waiting;
        LevelEndBattleUI(false);
        _successUICanvas.SetActive(false);
        _failUICanvas.SetActive(false);
        _selectionUICanvas.SetActive(false);
        _tapToPlayCanvas.gameObject.SetActive(true);
        _tapToPlayCanvas.DOFade(0, 1f).
            SetEase(Ease.OutCirc).OnComplete(() =>
            {
                _tapToPlayCanvas.DOFade(1, 1f);
            }).SetLoops(-1, LoopType.Yoyo);

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //change Game State first click.
        {
            if (!_isEnabled)
            {
                _isEnabled = true;
                TapToPlayFade();


                StateEnums.currentGameState = StateEnums.GameStateEnums.Playing;
            }
        }
    }

    private void TapToPlayFade()
    {
        DOTween.KillAll();
        _tapToPlayCanvas.DOFade(0, .5f).
            SetEase(Ease.OutCirc).OnComplete(() => _tapToPlayCanvas.gameObject.SetActive(false));
    }

    public void LevelEndBattleUI(bool state) // Open score table when runner end
    {
        _enemyUICanvas.SetActive(state);
        _playerUICanvas.SetActive(state);
    }

    private void LevelFail(bool state)
    {
        _failUICanvas.SetActive(true);
        LevelEndBattleUI(false);
    }
    private void LevelSuccess()
    {
        _successUICanvas.SetActive(true);
        _selectionUICanvas.SetActive(false);
        LevelEndBattleUI(false);
    }
    public void SelectionUICanvas(bool state) // open or close monster selection UI
    {
        _selectionUICanvas.SetActive(state);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetScoreText(bool _enemyScoreBool, bool _playerScoreBool, EnemyBehaviour enemyBehaviour) //Set scoren when monster battle
    {
        if (enemyBehaviour._monsters.Count == 0)
        {
            if (_playerScoreBool && _enemyScoreBool)
            {
                _playerCurrentScore += 1;
                _playerScoreText.text = "" + _playerCurrentScore;
                _enemyCurrentScore += 1;
                _enemyScoreText.text = "" + _enemyCurrentScore;

                CheckLevelState();

            }
            else if (_enemyScoreBool)
            {
                _enemyCurrentScore += 1;
                _enemyScoreText.text = "" + _enemyCurrentScore;
            }
            else if (_playerScoreBool)
            {
                _playerCurrentScore += 1;
                _playerScoreText.text = "" + _playerCurrentScore;
            }

            CheckLevelState();

        }

        if (_playerScoreBool && _enemyScoreBool)
        {
            _enemyCurrentScore += 1;
            _enemyScoreText.text = "" + _enemyCurrentScore;
            _playerCurrentScore += 1;
            _playerScoreText.text = "" + _playerCurrentScore;
            return;
        }
        else if (_enemyScoreBool)
        {
            _enemyCurrentScore += 1;
            _enemyScoreText.text = "" + _enemyCurrentScore;
        }
        else if (_playerScoreBool)
        {
            _playerCurrentScore += 1;
            _playerScoreText.text = "" + _playerCurrentScore;
        }



    }
    private void CheckLevelState() //check level state- Fail-Success
    {
        if (_enemyCurrentScore < _playerCurrentScore)
        {
            LevelSuccess();
            StateEnums.currentGameState = StateEnums.GameStateEnums.Finish;
            return;
        }
        else if (_playerCurrentScore < _enemyCurrentScore)
        {
            LevelFail(true);
            StateEnums.currentGameState = StateEnums.GameStateEnums.Finish;
            return;
        }

        else
        {
            LevelSuccess();
            StateEnums.currentGameState = StateEnums.GameStateEnums.Finish;
            return;
        }
    }


}
