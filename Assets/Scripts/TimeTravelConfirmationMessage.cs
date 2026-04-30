using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TimeTravelConfirmationMessage : MonoBehaviour
{
    private string targetScene;
    private int selectedIndex;
    private int prevSelected;

    public TextMeshProUGUI eraText;
    public CharacterMovement characterScript;
    public Button confirmButton;
    public Button cancelButton;
    public bool joyStickMode;
    //for the transition
    public Image fadeImage;
    public float fadeDuration = 0.8f;

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
        selectedIndex = 0;
        prevSelected = 0;
        UpdateButtonHighlight();
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
        AudioManager.Instance.PlayTimeTravel();
        StartCoroutine(FadeAndLoad());
    }

    private IEnumerator FadeAndLoad()
    {
        enabled = false;

        float elapsed = 0f;
        Color c = fadeImage.color;
        Debug.Log("Fade started, initial alpha: " + c.a);
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            Debug.Log("Fade going, alpha: " + c.a);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1f;
        fadeImage.color = c;
        SceneManager.LoadScene(targetScene);
    }

    public void Cancel()
    {
        AudioManager.Instance.PlayClick();
        gameObject.SetActive(false);
    }

    void UpdateButtonHighlight()
    {
        if (prevSelected != selectedIndex)
        {
            AudioManager.Instance.PlayHighlight();
            prevSelected = selectedIndex;
        }
        ColorBlock selected = ColorBlock.defaultColorBlock;
        selected.normalColor = Color.yellow;

        ColorBlock normal = ColorBlock.defaultColorBlock;
        normal.normalColor = Color.white;

        confirmButton.colors = (selectedIndex == 0) ? selected : normal;
        cancelButton.colors = (selectedIndex == 1) ? selected : normal;
    }

}
