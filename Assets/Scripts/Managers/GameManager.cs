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

    public float countdownTostartTimer { get; set; } = 5;

    // Events
    public static event Action<GameState> OnGameStateChanged;
    public event EventHandler OnGameOver;
    public event EventHandler OnVictory;
    public event EventHandler OnLevelIncreased;
    public event EventHandler OnLivesChanged;
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

    void Update()
    {
        if (CurrentGameState == GameState.Countdown)
        {
            UpdateCountdown();
        }

    }

    private void UpdateCountdown()
    {
        countdownTostartTimer -= Time.deltaTime;
        if (countdownTostartTimer < 0f)
        {
            SetGameState(GameState.Playing);
        }
    }

    public void SetGameState(GameState newState)
    {
        CurrentGameState = newState;
        Debug.Log("Game state changed to: " + CurrentGameState);

        switch (newState)
        {
            case GameState.WaitingToStart:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.Countdown:

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameState.Playing:
                break;
            case GameState.EndGame:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }

        OnGameStateChanged?.Invoke(CurrentGameState);
    }

    public void EnemyKilled()
    {
        amountKilledEnemies++;
        Debug.Log("Enemies killed: " + amountKilledEnemies);

        if (amountKilledEnemies >= 55)
        {
            Lives++;
            OnLivesChanged?.Invoke(this, EventArgs.Empty);
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
        OnLivesChanged?.Invoke(this, EventArgs.Empty);
    }

    public void IncreaseLevel()
    {
        CurrentLevel++;
        Debug.Log("Level increased to: " + CurrentLevel);

        if (CurrentLevel > 10)
        {
            CurrentLevel = 10;
            OnVictory?.Invoke(this, EventArgs.Empty);
            SetGameState(GameState.EndGame);
        }
        else
        {
            amountKilledEnemies = 0;
        }
        OnLevelIncreased?.Invoke(this, EventArgs.Empty);

    }

}
