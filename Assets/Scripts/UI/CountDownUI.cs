
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    private int previousCountdownNumber;



    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Countdown)
            return;

        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.countdownTostartTimer);

        countdownText.text = countdownNumber.ToString();

        if (previousCountdownNumber != countdownNumber)
        {
            previousCountdownNumber = countdownNumber;
        }

    }
}
