using UnityEngine;

public class ParkRanger : MonoBehaviour
{
    private Outline outline;
    private bool pointerIn;

    public GameObject rangerMenu;
    public Transform player;
    public float interactDistance = 5f;

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
                if (rangerMenu != null && player != null)
                {
                    float distance = Vector3.Distance(player.position, transform.root.position);

                    if (distance > interactDistance)
                    {
                        Debug.Log("Too far from ranger to interact");
                        return;
                    }

                    AudioManager.Instance.PlayClick();

                    Vector3 direction = Camera.main.transform.position - rangerMenu.transform.position;
                    direction.y = 0;
                    //rangerMenu.transform.rotation = Quaternion.LookRotation(direction);

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