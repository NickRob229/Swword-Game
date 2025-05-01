using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectMenu : MonoBehaviour
{
    public GameObject weaponMenuUI;
    public Button shortswordButton;
    public Button longswordButton;
    public Button rapierButton;
    public Button sabreButton;

    public PlayerMovement playerMovement; // Reference to your player movement script

    private void Start()
    {
        // Pause player input and show menu
        weaponMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playerMovement.enabled = false;

        // Assign button events
        shortswordButton.onClick.AddListener(() => SelectWeapon("Short Sword"));
        longswordButton.onClick.AddListener(() => SelectWeapon("Greatsword"));
        rapierButton.onClick.AddListener(() => SelectWeapon("Katana"));
        sabreButton.onClick.AddListener(() => SelectWeapon("Scimitar"));
    }

    void SelectWeapon(string weaponName)
    {
        Debug.Log("Selected Weapon: " + weaponName);

        // Hide menu
        weaponMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable movement
        playerMovement.enabled = true;

        // Store or process selected weapon (e.g. save to a string/enum)
        // Later, you'll spawn or attach the sword model here
    }
}
