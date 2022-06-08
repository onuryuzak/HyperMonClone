using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnums : MonoBehaviour
{
    public static GameStateEnums currentGameState;
    public enum GameStateEnums
    {
        Waiting,
        Playing,
        Finish
    }



}
