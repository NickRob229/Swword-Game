using UnityEngine;
using UnityEngine.UI;

public class MenuButtonHandler : MonoBehaviour
{
    [Header("Main Menu Panel")]
    public GameObject mainMenuPanel;

    [Header("Second Menu Panel")]
    public GameObject secondMenuPanel;

    [Header("Player Settings")]
    public Transform playerTransform;
    public Vector3 playerStartPosition;

    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public Vector3 enemySpawnPosition;

    private GameObject currentEnemyInstance;

    public void OnStartButton()
    {
        // Disable the main menu
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        // Enable the second menu
        if (secondMenuPanel != null)
            secondMenuPanel.SetActive(true);

        // Move the player to the starting position
        if (playerTransform != null)
        {
            CharacterController controller = playerTransform.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;

            playerTransform.position = playerStartPosition;

            if (controller != null) controller.enabled = true;

            Debug.Log("Resetting player position to: " + playerStartPosition);
        }
        else
        {
            Debug.LogWarning("Player Transform is not assigned!");
        }

        // Spawn the enemy at the spawn position
        if (enemyPrefab != null)
        {
            GameObject spawnedEnemy = Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity);
            spawnedEnemy.tag = "EnemyClone"; // Assign the tag after instantiation
            Debug.Log("Spawned enemy at: " + enemySpawnPosition);
        }
        else
        {
            Debug.LogWarning("Enemy Prefab is not assigned!");
        }

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
