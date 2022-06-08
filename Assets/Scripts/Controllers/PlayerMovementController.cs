using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class PlayerMovementController : MonoBehaviour
{

    [HideInInspector] public Vector3 fingerFirstPosition;
    [HideInInspector] public Vector3 fingerDirection;
    [SerializeField] private float _inputSensivity;
    [SerializeField] private float _rotationAngle;
    private SplineFollower _splineFollower;
    private PlayerAnimationController _playerAnimationController;
    private const string _splineComputerString = "SplineComputer";
    private bool _isEnabled;
    public bool _isMovement;
    void Awake()
    {
        _playerAnimationController = GetComponent<PlayerAnimationController>();
        _splineFollower = GetComponent<SplineFollower>();
        _splineFollower.spline = GameObject.FindGameObjectWithTag(_splineComputerString).GetComponent<SplineComputer>();
    }
    private void Start()
    {
        _isMovement = true;
    }
    void Update()
    {
        if (!(StateEnums.currentGameState == StateEnums.GameStateEnums.Playing)) return;

        SwerveInput();
        if (Input.GetMouseButtonDown(0))
        {
            if (!_isEnabled)
            {
                _splineFollower.follow = true;
                _playerAnimationController.Running(true);
            }
            _isEnabled = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (_splineFollower != null && _isMovement)
            {
                _splineFollower.motion.offset = new Vector2(_splineFollower.motion.offset.x + (fingerDirection.x * _inputSensivity), _splineFollower.motion.offset.y);
                Vector2 newPos = _splineFollower.motion.offset;
                newPos.x = Mathf.Clamp(newPos.x, -3.4f, 2.5f);
                _splineFollower.motion.offset = newPos;
                if (fingerDirection.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, -_rotationAngle, 0);
                }
                else if (0 < fingerDirection.x)
                {
                    transform.rotation = Quaternion.Euler(0, _rotationAngle, 0);

                }
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }




    public void SwerveInput() //Touch InputController
    {
        if (Input.GetMouseButtonDown(0))
        {
            fingerFirstPosition = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0);
        }
        else if (Input.GetMouseButton(0))
        {
            fingerDirection = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0) - fingerFirstPosition;
            fingerFirstPosition = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            fingerDirection = Vector3.zero;
        }
    }
}
