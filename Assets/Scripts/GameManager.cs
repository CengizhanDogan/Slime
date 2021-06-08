using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState;
    public AiState aiState;

    private void Awake()
    {
        Instance = this;
    }
}

public enum GameState
{
    Slime,
    Sword,
    Death,
    End
}

public enum AiState
{
    Move,
    Attack,
    Death
}

