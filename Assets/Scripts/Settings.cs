using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Settings : MonoBehaviour
{
    private Button[] menuButtons;
    private int buttonIndex;
    private int numMenuButtons;
    private float joystickTimer;
    private float joystickDelay;
    private Dictionary<float, string> speedMappings = new Dictionary<float, string>
    {
        { 20f, "High" },
        { 10f, "Medium" },
        { 5f, "Low" }
    };
    private float[] speeds = { 5f, 10f, 20f };
    private int speedIndex;

    public CharacterMovement characterMovement;
    public GameObject menu;
    public CameraOperations cameraScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("playerSpeed"))
        {
            float savedSpeed = PlayerPrefs.GetFloat("playerSpeed");
            for (int i = 0; i < speeds.Length; i++)
            {
                if (speeds[i] == savedSpeed)
                {
                    speedIndex = i;
                    break;
                }
            }
        }
        else
        {
            speedIndex = 0;
        }
        joystickDelay = 0.3f;
        joystickTimer = 0;
        UpdateSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (joystickTimer > 0)
        {
            joystickTimer -= Time.deltaTime;
        }
        //joystick navigation
        float vertical = Input.GetAxis("Horizontal") * -1;
        if (joystickTimer <= 0)
        {
            if (vertical > 0.5f && buttonIndex > 0) //up
            {
                menuButtons[buttonIndex].GetComponent<Image>().color = Color.white;
                buttonIndex--;
                menuButtons[buttonIndex].GetComponent<Image>().color = Color.yellow;
                joystickTimer = joystickDelay;
            }
            else if (vertical < -0.5f && buttonIndex < numMenuButtons - 1) // down
            {
                menuButtons[buttonIndex].GetComponent<Image>().color = Color.white;
                buttonIndex++;
                menuButtons[buttonIndex].GetComponent<Image>().color = Color.yellow;
                joystickTimer = joystickDelay;
            }

        }
        //menu selection
        if (Input.GetButtonDown("js1") || Input.GetKeyDown(KeyCode.B))
        {
            string buttonText = menuButtons[buttonIndex].GetComponentInChildren<TMP_Text>().text;

            if (string.Equals(buttonText, "Back to Menu"))
            {
                menu.SetActive(true);
            
                gameObject.SetActive(false);
            }
            else if (buttonText.Contains("Resume"))
            {
                characterMovement.enabled = true;
                cameraScript.enabled = true;
                gameObject.SetActive(false);
                
            }
            else if (buttonText.Contains("Speed"))
            {
                speedIndex++;
                speedIndex = speedIndex % speeds.Length;

                UpdateSpeed();
            }
            else if (string.Equals(buttonText, "Quit"))
            {
                Application.Quit();
            }
        }
    }

    void OnEnable()
    {
        characterMovement.enabled = false;
        cameraScript.enabled = false;
        buttonIndex = 0;
        menuButtons = transform.GetComponentsInChildren<Button>();
        menuButtons[buttonIndex].GetComponent<Image>().color = Color.yellow;

        numMenuButtons = menuButtons.Length;
        for (int i = 1; i < menuButtons.Length; i++)
        {
            menuButtons[i].GetComponent<Image>().color = Color.white;

        }
    }

    void UpdateSpeed()
    {
        characterMovement.speed = speeds[speedIndex];
        menuButtons[2].GetComponentInChildren<TMP_Text>().text = "Speed: " + speedMappings[speeds[speedIndex]];
        PlayerPrefs.SetFloat("playerSpeed", speeds[speedIndex]);
    }
}
