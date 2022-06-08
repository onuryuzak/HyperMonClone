using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CinemachineCameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playerFollowCam;
    [SerializeField] private CinemachineVirtualCamera _battleCam;
    [SerializeField] private int _targetPlayerCMFOV;
    private float _shakeTimer;
    private float _shakeTimerTotal;
    private float _startingIntensity;
    private void OnEnable()
    {
        EventManager.OnPlayerMoveBackward += ShakeCinemachine;
        EventManager.OnChangeCameraFOV += SetFOVPlayerCam;
        EventManager.OnRunnerFinish += RunnerFinish;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerMoveBackward -= ShakeCinemachine;
        EventManager.OnChangeCameraFOV -= SetFOVPlayerCam;
        EventManager.OnRunnerFinish -= RunnerFinish;
    }
    void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                _playerFollowCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(_startingIntensity, 0f, 1 - _shakeTimer / _shakeTimerTotal);
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            _playerFollowCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        _startingIntensity = intensity;
        _shakeTimerTotal = time;
        _shakeTimer = time;
    }

    private void ShakeCinemachine()
    {
        ShakeCamera(1f, 1f);
    }
    public void SetFOVPlayerCam()
    {

        float currentFovValue = _playerFollowCam.m_Lens.FieldOfView;

        DOVirtual.Float(currentFovValue, _targetPlayerCMFOV, 2f, t =>
        {
            _playerFollowCam.m_Lens.FieldOfView = t;
        });
    }

    private void RunnerFinish() //Change camera when runner finish
    {
        _playerFollowCam.m_Priority = 1;
    }
}
