using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text winnerText;

    void Awake()
    {
        Instance = this;
        gameOverPanel.SetActive(false);
    }

    public void ShowResult(ulong winnerId, bool isLocalPlayerWinner)
    {
        gameOverPanel.SetActive(true);

        winnerText.text = isLocalPlayerWinner
            ? "You Win!"
            : $"Player {winnerId} Wins!";
    }
}