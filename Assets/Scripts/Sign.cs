using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sign : MonoBehaviour
{
    private bool pointerIn = false;
    private float minDistance = 2.5f;
    private Outline outline;

    public bool signExpanded;
    public GameObject expandedSign;
    public string signTitle;
    public string signDescription;
    public TextMeshPro titleText;
    public TextMeshProUGUI descriptionText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointerIn = false;
        signExpanded = false;
        titleText.text = signTitle;
        descriptionText.text = signDescription;
        outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (signExpanded)
        {
            return;
        }

        if (pointerIn)
        {
            outline.enabled = true;
            Transform cam = Camera.main.transform;
            float distance = Vector3.Distance(cam.position, transform.position);

            if (distance > minDistance)
            {
                if (Input.GetButtonDown("js1") || Input.GetKeyDown(KeyCode.B))
                {
                    expandedSign.transform.position = cam.position + cam.forward * 1f;
                    expandedSign.transform.rotation = Quaternion.LookRotation(cam.forward);

                    expandedSign.SetActive(true);
                    signExpanded = true;
                    outline.enabled = false;
                }
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
