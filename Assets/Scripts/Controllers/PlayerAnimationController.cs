using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _anim;
    private const string runningAnimString = "Run";
    private const string fallingDownAnimString = "FallingDown";
    private const string throwAnimString = "Throw";


    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    public void Running(bool state)
    {
        _anim.SetBool(runningAnimString, state);
    }
    public void Backward(bool state)
    {
        _anim.SetBool(fallingDownAnimString, state);
    }
    public void Throw(bool state)
    {
        _anim.SetBool(throwAnimString, state);
    }
}
