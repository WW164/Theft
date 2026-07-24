using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private GameTimer gameTimer;

    void Update()
    {
        if (gameTimer == null)
        {
            Debug.Log("Game Timer Spawned!");
            gameTimer = FindObjectOfType<GameTimer>();
            if (gameTimer == null) return;
        }

        float time = gameTimer.TimeRemaining.Value;
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}