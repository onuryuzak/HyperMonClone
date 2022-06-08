using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GateController : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] Image _monsterImage;
    [SerializeField] TextMeshProUGUI _gatePriceText;
    [SerializeField] TextMeshProUGUI _monsterPowerText;
    [SerializeField] TextMeshProUGUI _monsterNameText;
    public MonsterType _monsterType;



    private void Start()
    {
        _gatePriceText.text = "" + _price;
        switch (_monsterType) //Set Gate Image chosen monster type
        {
            case MonsterType.Bat:
                SetMonsterInfos(MonsterType.Bat);
                break;
            case MonsterType.Ghost:
                SetMonsterInfos(MonsterType.Ghost);
                break;
            case MonsterType.Phantom:
                SetMonsterInfos(MonsterType.Phantom);
                break;
            case MonsterType.Spider:
                SetMonsterInfos(MonsterType.Spider);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBehaviour playerBehaviour))
        {
            if (_price <= playerBehaviour.TotalPokeball) //if player has enough money, spawn chosen monster
            {
                ChooseMonster(playerBehaviour);
                VFXManager.instance.DustVFX(transform);

            }
            else // if player has not enough money, go backward a little bit
            {
                EventManager.PlayerMoveBackward();
            }
        }

    }
    private void ChooseMonster(PlayerBehaviour playerBehaviour) // called Triggerevent func. for spawn monster
    {
        switch (_monsterType)
        {
            case MonsterType.Bat:
                TriggerEvent(MonsterType.Bat, playerBehaviour, _price);
                break;
            case MonsterType.Ghost:
                TriggerEvent(MonsterType.Ghost, playerBehaviour, _price);
                break;
            case MonsterType.Phantom:
                TriggerEvent(MonsterType.Phantom, playerBehaviour, _price);
                break;
            case MonsterType.Spider:
                TriggerEvent(MonsterType.Spider, playerBehaviour, _price);
                break;
            default:
                Debug.LogError("Please Choose Monster Type!");
                break;
        }
    }

    private void TriggerEvent(MonsterType type, PlayerBehaviour playerBehaviour, int price)//Trigger SpawnMonster event
    {
        EventManager.SpawnMonster(type, playerBehaviour, price);
        Destroy(gameObject);
    }

    private void SetMonsterInfos(MonsterType monsterTypeName) //fetch monsters in file Resources/Monster
    {
        _monsterImage.sprite = Resources.Load<Sprite>("CardIcons/"+ monsterTypeName);
        _monsterPowerText.text = "" + Resources.Load<GameObject>("Monster/"+ monsterTypeName).GetComponent<MonsterBehaviour>().Power;
        _monsterNameText.text = "" + Resources.Load<GameObject>("Monster/"+ monsterTypeName).name;
    }



}

