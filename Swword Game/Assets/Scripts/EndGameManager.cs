using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    public GameObject endGameCanvas;
    public GameObject winnerText;
    public GameObject loserText;
    public Button goBackButton;
    public GameObject mainMenuCanvas;

    void Start()
    {
        // Ensure everything is disabled at start
        endGameCanvas.SetActive(false);
        winnerText.SetActive(false);
        loserText.SetActive(false);
        goBackButton.gameObject.SetActive(false);

        // Lock cursor on start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowWinnerScreen()
    {
        endGameCanvas.SetActive(true);
        winnerText.SetActive(true);
        loserText.SetActive(false);
        goBackButton.gameObject.SetActive(true);

        Time.timeScale = 0f;
        UnlockCursor();
    }

    public void ShowLoserScreen()
    {
        endGameCanvas.SetActive(true);
        winnerText.SetActive(false);
        loserText.SetActive(true);
        goBackButton.gameObject.SetActive(true);

        Time.timeScale = 0f;
        UnlockCursor();
    }

    public void ReturnToMainMenu()
    {
        endGameCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        Time.timeScale = 1f;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
