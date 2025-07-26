using System;
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

    public GameState CurrentGameState { get; private set; }

    [HideInInspector] public Vector3 leftBound { get; private set; }
    [HideInInspector] public Vector3 rightBound { get; private set; }

    private int amountKilledEnemies = 0;
    public int Lives { get; private set; } = 3;
    public int CurrentLevel { get; private set; } = 1;

    // Events
    public static event Action<GameState> OnGameStateChanged;
    public event EventHandler OnGameOver;
    public event EventHandler OnVictory;
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

    public void EnemyKilled()
    {
        amountKilledEnemies++;
        Debug.Log("Enemies killed: " + amountKilledEnemies);

        if (amountKilledEnemies == 55)
        {
            SetGameState(GameState.EndGame);
            Lives++;
            Debug.Log("Game Over! You killed enough enemies.");
            IncreaseLevel();

        }
    }

    public void PlayerDied()
    {
        Lives--;
        Debug.Log("Lives left: " + Lives);

        if (Lives < 0)
        {
            Lives = 0;
            SetGameState(GameState.EndGame);
            Debug.Log("Game Over! You have no lives left.");
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
    }

    public void IncreaseLevel()
    {
        CurrentLevel++;
        Debug.Log("Level increased to: " + CurrentLevel);

        if (CurrentLevel > 10)
        {
            OnVictory?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            amountKilledEnemies = 0;
        }

    }

}
