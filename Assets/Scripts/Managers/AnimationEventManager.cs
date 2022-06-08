using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System;

public class AnimationEventManager : MonoBehaviour
{
    private PlayerAnimationController _playerAnimationController;
    private PlayerMovementController _playerMovementController;
    private SplineFollower _splineFollower;
    private bool _levelFail;
    private void OnEnable()
    {
        EventManager.OnLevelFail += LevelFail;
    }
    private void OnDisable()
    {
        EventManager.OnLevelFail -= LevelFail;
    }

    private void Awake()
    {
        _playerAnimationController = GetComponentInParent<PlayerAnimationController>();
        _splineFollower = GetComponentInParent<SplineFollower>();
        _playerMovementController = GetComponentInParent<PlayerMovementController>();
    }
    public void ContinueFollow()
    {
        _playerAnimationController.Backward(false);
        _splineFollower.follow = true;
        _playerMovementController._isMovement = true;
        if (_levelFail)
        {
            _playerAnimationController.Running(false);
            _splineFollower.follow = false;
        }
    }
    public void PlayerThrowPokeball()
    {
        EventManager.PlayerThrowPokeball();
    }
    public void EnemyThrowPokeball()
    {
        EventManager.EnemyThrowPokeball();
    }
    private void LevelFail(bool state)
    {
        _levelFail = state;
    }
}
