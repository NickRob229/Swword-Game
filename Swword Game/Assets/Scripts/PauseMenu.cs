using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu Panel")]
    public GameObject pauseMenuPanel;

    [Header("Main Menu Panel")]
    public GameObject mainMenuPanel;

    private bool isPaused = false;

    void Start()
    {
        // Ensure pause menu is hidden at start
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Right Enter key
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    public void OnContinueButton()
    {
        TogglePause();
    }

    public void OnReturnToMenuButton()
    {
        // Disable pause menu
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);

        // Show main menu
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Destroy all enemy clones
        GameObject[] clones = GameObject.FindGameObjectsWithTag("EnemyClone");
        foreach (GameObject clone in clones)
        {
            Destroy(clone);
        }
    }
}
