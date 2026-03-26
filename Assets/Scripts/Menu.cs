using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private int selectedIndex;

    public CharacterMovement characterScript;
    public Button resumeButton;
    public Button settingsButton;
    public GameObject settingsMenu;
    public CameraOperations cameraScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Vertical");

        if (horizontal < -0.5f)
        {
            selectedIndex = 0;
            UpdateButtonHighlight();
        }
        else if (horizontal > 0.5f)
        {
            selectedIndex = 1;
            UpdateButtonHighlight();
        }

        if (Input.GetButtonDown("js1") || Input.GetKeyDown(KeyCode.B))
        {
            if (selectedIndex == 0)
            {
                Resume();
            }
            else
            {
                OpenSettings();
            }

        }
    }

    void OnEnable()
    {
        selectedIndex = 0;
        UpdateButtonHighlight();
        characterScript.enabled = false;
        cameraScript.enabled = false;
    }

    public void Resume()
    {
        characterScript.enabled = true;
        cameraScript.enabled = true;
        gameObject.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    void UpdateButtonHighlight()
    {
        ColorBlock selected = ColorBlock.defaultColorBlock;
        selected.normalColor = Color.yellow;

        ColorBlock normal = ColorBlock.defaultColorBlock;
        normal.normalColor = Color.white;

        resumeButton.colors = (selectedIndex == 0) ? selected : normal;
        settingsButton.colors = (selectedIndex == 1) ? selected : normal;
    }
}
