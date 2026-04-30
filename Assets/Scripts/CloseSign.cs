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
                gameObject.SetActive(false);
            }
        }
    }

    void OnEnable()
    {
        AudioManager.Instance.PlayClick();
        characterScript.enabled = false;
        cameraScript.enabled = false;
        pointerIn = false;
        gameObject.GetComponent<Image>().color = Color.white;
    }

    void OnDisable()
    {
        gameObject.GetComponent<Image>().color = Color.white;
        AudioManager.Instance.PlayClick();
        signScript.signExpanded = false;
        expandedSign.SetActive(false);
        characterScript.enabled = true;
        cameraScript.enabled = true;
    }

    public void OnPointerEnter()
    {
        gameObject.GetComponent<Image>().color = Color.yellow;
        
        pointerIn = true;
        AudioManager.Instance.PlayHighlight();
    }

    public void OnPointerExit()
    {
        pointerIn = false;
        gameObject.GetComponent<Image>().color = Color.white;
        AudioManager.Instance.PlayUnhighlight();
    }
}
