using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MonsterBehaviour : MonoBehaviour
{
    public int id;
    public int Power;
    public bool isBattleMonster;
    public Transform rigModel;

    private void Start()
    {
        if (isBattleMonster) GetComponentInChildren<TextMeshProUGUI>().text = "" + Power;
    }
}
