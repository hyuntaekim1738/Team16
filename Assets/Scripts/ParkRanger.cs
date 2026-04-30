using UnityEngine;

public class ParkRanger : MonoBehaviour
{
    private Outline outline;
    private bool pointerIn;

    public GameObject rangerMenu;

    void Start()
    {
        outline = gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    void Update()
    {
        if (pointerIn)
        {
            if (outline != null)
            {
                outline.enabled = true;
            }

            if (Input.GetButtonDown("js1") || Input.GetKeyDown(KeyCode.B))
            {
                if (rangerMenu != null)
                {
                    AudioManager.Instance.PlayClick();
                    rangerMenu.transform.rotation = Quaternion.LookRotation(rangerMenu.transform.position - Camera.main.transform.position);
                    rangerMenu.SetActive(true);
                    outline.enabled = false;
                }
            }
        }
        else
        {
            if (outline != null)
            {
                outline.enabled = false;
            }
        }
    }

    void OnEnable()
    {
        pointerIn = false;
    }

    public void OnPointerEnter()
    {
        pointerIn = true;
        if (!rangerMenu.activeInHierarchy)
        {
            AudioManager.Instance.PlayHighlight();
        }
    }

    public void OnPointerExit()
    {
        pointerIn = false;
        if (!rangerMenu.activeInHierarchy)
        {
            AudioManager.Instance.PlayUnhighlight();
        }
    }
}