using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public GameState CurrentGameState;

    public Vector3 leftBound;
    public Vector3 rightBound;
    public static event Action<GameState> OnGameStateChanged;
    public enum GameState
    {
        WaitingToStart,
        Countdown,
        Playing,
        EndGame
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        leftBound = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightBound = Camera.main.ViewportToWorldPoint(Vector3.right);

        SetGameState(GameState.WaitingToStart);
    }

    public void SetGameState(GameState newState)
    {
        CurrentGameState = newState;
        Debug.Log("Game state changed to: " + CurrentGameState);

        switch (newState)
        {
            case GameState.WaitingToStart:
                break;
            case GameState.Countdown:
                break;
            case GameState.Playing:
                break;
            case GameState.EndGame:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

}
