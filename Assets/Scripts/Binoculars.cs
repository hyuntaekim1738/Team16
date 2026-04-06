using UnityEngine;

public class Binoculars : MonoBehaviour
{
    private Outline outline;
    private bool pointerIn;
    private float interactRange = 5f;

    public GameObject binocularsOverlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        bool inRange = distance <= interactRange;
        if (pointerIn && inRange)
        {
            outline.enabled = true;
            if (Input.GetButtonDown("js1") || Input.GetKeyDown(KeyCode.B))
            {
                binocularsOverlay.SetActive(true);
                outline.enabled = false;
            }
        }
        else
        {
            outline.enabled = false;
        }
    }

    void OnEnable()
    {
        pointerIn = false;
    }

    public void OnPointerEnter()
    {
        pointerIn = true;
    }

    public void OnPointerExit()
    {
        pointerIn = false;
    }
}
