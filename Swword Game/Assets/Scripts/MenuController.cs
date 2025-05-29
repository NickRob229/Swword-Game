using UnityEngine;
using UnityEngine.UI;

public class MenuButtonHandler : MonoBehaviour
{
    [Header("Main Menu Panel")]
    public GameObject mainMenuPanel;

    [Header("Second Menu Panel")]
    public GameObject secondMenuPanel;

    public void OnStartButton()
    {
        // Disable the main menu
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        // Enable the second menu
        if (secondMenuPanel != null)
            secondMenuPanel.SetActive(true);
    }

    public void OnQuitButton()
    {
        Debug.Log("Quitting Application...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}