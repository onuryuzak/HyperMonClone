using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public int id;
    public Transform parentTransform;

    private void Start()
    {
        parentTransform = transform.parent.parent;
    }
}
