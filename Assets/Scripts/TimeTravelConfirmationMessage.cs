using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TimeTravelConfirmationMessage : MonoBehaviour
{
    private string targetScene;
    private int selectedIndex = 0;

    public TextMeshProUGUI eraText;
    public CharacterMovement characterScript;
    public Button confirmButton;
    public Button cancelButton;
    public bool joyStickMode;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (joyStickMode)
        {
            horizontal = Input.GetAxis("Vertical");
        }

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

        if (Input.GetButtonDown("js1") || Input.GetKeyDown(KeyCode.B)) //Input.GetButtonDown("js0") ||
        {
            if (selectedIndex == 0)
            {
                Confirm();
            }    
            else
            {
                Cancel();
            }
                
        }
    }

    void OnEnable()
    {
        characterScript.enabled = false;
    }

    void OnDisable()
    {
        characterScript.enabled = true;
    }

    public void Setup(string eraName, string scene)
    {
        targetScene = scene;
        eraText.text = "Would you like to travel to era: " + eraName + "?";
        selectedIndex = 0;
        UpdateButtonHighlight();
    }

    public void Confirm()
    {
        SceneManager.LoadScene(targetScene);
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }

    void UpdateButtonHighlight()
    {
        ColorBlock selected = ColorBlock.defaultColorBlock;
        selected.normalColor = Color.yellow;

        ColorBlock normal = ColorBlock.defaultColorBlock;
        normal.normalColor = Color.white;

        confirmButton.colors = (selectedIndex == 0) ? selected : normal;
        cancelButton.colors = (selectedIndex == 1) ? selected : normal;
    }

}
