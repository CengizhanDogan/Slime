using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState;

    private void Awake()
    {
        Instance = this;
    }
}

public enum GameState
{
    Idle,
    Slime,
    Sword,
    Death,
    End
}

