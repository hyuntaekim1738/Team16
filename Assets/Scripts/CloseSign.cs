using UnityEngine;
using UnityEngine.UI;

public class CloseSign : MonoBehaviour
{
    private bool pointerIn;

    public Sign signScript;
    public GameObject expandedSign;
    public CharacterMovement characterScript;
    public CameraOperations cameraScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointerIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointerIn)
        {
            if (Input.GetButtonDown("js1") || Input.GetKeyDown(KeyCode.B))
            {
                signScript.signExpanded = false;
                expandedSign.SetActive(false);
                characterScript.enabled = true;
                cameraScript.enabled = true;
            }
        }
    }

    void OnEnable()
    {
        characterScript.enabled = false;
        cameraScript.enabled = false;
        gameObject.GetComponent<Image>().color = Color.white;
    }

    public void OnPointerEnter()
    {
        gameObject.GetComponent<Image>().color = Color.yellow;
        
        pointerIn = true;
    }

    public void OnPointerExit()
    {
        pointerIn = false;
        gameObject.GetComponent<Image>().color = Color.white;
    }
}
