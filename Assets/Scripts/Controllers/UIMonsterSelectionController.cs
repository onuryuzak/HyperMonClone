using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIMonsterSelectionController : MonoBehaviour
{
    public List<Image> selectionFieldMonsterImage;
    public List<TextMeshProUGUI> selectionFieldPowerText;
    PlayerBehaviour playerBehaviour;
    private int _index = 0;
    private void OnEnable()
    {
        EventManager.OnStartBattle += GetTakenMonsters;
    }
    private void OnDisable()
    {
        EventManager.OnStartBattle -= GetTakenMonsters;
    }

    private void GetTakenMonsters() //Get taken monsters by player
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        foreach (MonsterBuilder item in playerBehaviour.takenMonsters)
        {
            SetMonsterUI(item._monsterType, playerBehaviour.takenMonsters.IndexOf(item), playerBehaviour);

        }
    }

    private void SetMonsterUI(MonsterType monsterTypeName, int index, PlayerBehaviour playerBehaviour) //fetch monsters info for selection UI
    {

        selectionFieldMonsterImage[index].sprite = Resources.Load<Sprite>("CardIcons/" + monsterTypeName);
        selectionFieldPowerText[index].text = "" + Resources.Load<GameObject>("Monster/" + monsterTypeName).GetComponent<MonsterBehaviour>().Power;
    }

    public void SetNewButtonID()
    {
        foreach (MonsterBuilder item in playerBehaviour.takenMonsters)
        {

            int index = playerBehaviour.takenMonsters.IndexOf(item);
            selectionFieldMonsterImage[index].GetComponentInChildren<ButtonController>().id = playerBehaviour.takenMonsters[index].GetComponentInChildren<MonsterBehaviour>().id;

        }

    }
}
