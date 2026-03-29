using UnityEngine;

public class Binoculars : MonoBehaviour
{
    private Outline outline;
    private bool pointerIn;

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
        if (pointerIn)
        {
            outline.enabled = true;
            if (Input.GetButtonDown("js1") || Input.GetKeyDown(KeyCode.B))
            {
                binocularsOverlay.SetActive(true);
            }
        }
        else
        {
            outline.enabled = false;
        }
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
