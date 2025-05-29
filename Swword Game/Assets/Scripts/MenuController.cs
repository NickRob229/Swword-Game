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

    [Header("Enemy Health UI")]
    public GameObject enemyHealthSliderPrefab; // Slider prefab to instantiate
    public Canvas targetCanvas; // Assign the specific Canvas here in Inspector

    private GameObject currentEnemyInstance;
    private GameObject currentEnemyHealthSlider;

    public void OnStartButton()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (secondMenuPanel != null) secondMenuPanel.SetActive(true);

        if (playerTransform != null)
        {
            CharacterController controller = playerTransform.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;

            playerTransform.position = playerStartPosition;

            if (controller != null) controller.enabled = true;

            Debug.Log("Player reset to start position: " + playerStartPosition);
        }
        else
        {
            Debug.LogWarning("Player Transform is not assigned!");
        }

        if (currentEnemyInstance != null) Destroy(currentEnemyInstance);
        if (currentEnemyHealthSlider != null) Destroy(currentEnemyHealthSlider);

        if (enemyPrefab != null && enemyHealthSliderPrefab != null)
        {
            currentEnemyInstance = Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity);
            currentEnemyInstance.tag = "EnemyClone";

            if (targetCanvas != null)
            {
                currentEnemyHealthSlider = Instantiate(enemyHealthSliderPrefab, targetCanvas.transform);
                Slider sliderComponent = currentEnemyHealthSlider.GetComponent<Slider>();

                EnemyHealth enemyHealth = currentEnemyInstance.GetComponent<EnemyHealth>();
                if (enemyHealth != null && sliderComponent != null)
                {
                    enemyHealth.healthBar = sliderComponent;
                }
                else
                {
                    Debug.LogWarning("EnemyHealth or Slider component missing!");
                }
            }
            else
            {
                Debug.LogWarning("Target Canvas is not assigned!");
            }

            Debug.Log("Spawned enemy and health slider at: " + enemySpawnPosition);
        }
        else
        {
            Debug.LogWarning("Enemy prefab or enemy health slider prefab is not assigned!");
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
