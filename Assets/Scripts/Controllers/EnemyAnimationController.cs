using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _anim;
    private const string throwAnimString = "Throw";

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void Throw(bool state)
    {
        _anim.SetBool(throwAnimString, state);
    }
}
