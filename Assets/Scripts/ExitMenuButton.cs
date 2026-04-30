using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExitMenuButton : MonoBehaviour
{
    public GameObject menu;
    public Button b;
    public GameObject character;
    public CameraOperations cameraScript;
    bool isPointerEnter = false;
    static bool menuClosed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        menuClosed = false;
        if(isPointerEnter)
        {
            b.GetComponent<Image>().color = new Color(255,255,0);
            if (Input.GetButtonDown("js1"))
            {
                character.GetComponent<CharacterMovement>().enabled = true;
                cameraScript.enabled = true;
                menuClosed = true;
                menu.SetActive(false);
            }
        }
        else
        {
            if(!menuClosed){
                b.GetComponent<Image>().color = new Color(255,255,255);
            }
        }
        // Debug.Log("MENU CLOSED? " + menuClosed);
    }

    public void OnPointerEnter()
    {
        isPointerEnter = true;
    }

    public void OnPointerExit()
    {
        isPointerEnter = false;
    }

    public static bool animalMenuClosed()
    {
        return menuClosed;
    }
}
