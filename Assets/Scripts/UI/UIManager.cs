using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject livesPanel;
    [SerializeField] private TextMeshProUGUI endGameText;
    [SerializeField] private GameObject endGamePanel;

    [SerializeField] private TextMeshProUGUI countdownText;

    void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        GameManager.Instance.OnGameOver += OnGameOver;
        GameManager.Instance.OnVictory += OnVictory;
        GameManager.Instance.OnLivesChanged += OnLivesChanged;

        livesPanel.gameObject.SetActive(false);
        endGamePanel.gameObject.SetActive(false);
        livesText.text = GameManager.Instance.Lives.ToString();
        countdownText.gameObject.SetActive(false);

    }

    private void OnLivesChanged(object sender, EventArgs e)
    {
        livesText.text = GameManager.Instance.Lives.ToString();
    }

    private void OnVictory(object sender, EventArgs e)
    {
        endGameText.text = "VICTORY";

    }

    private void OnGameOver(object sender, EventArgs e)
    {
        endGameText.text = "GAME OVER";
    }



    private void OnGameStateChanged(GameManager.GameState state)
    {
        countdownText.gameObject.SetActive(state == GameManager.GameState.Countdown);
        startPanel.gameObject.SetActive(state == GameManager.GameState.WaitingToStart);
        livesPanel.gameObject.SetActive(state == GameManager.GameState.Playing);
        endGamePanel.gameObject.SetActive(state == GameManager.GameState.EndGame);
    }



    public void Quit()
    {
        Application.Quit();

    }

    public void StartGame()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Countdown);
    }


}
