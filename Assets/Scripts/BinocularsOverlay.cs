using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class BinocularsOverlay : MonoBehaviour
{
    private Dictionary<float, string> zoomMappings = new Dictionary<float, string>
    {
        { 60f, "None" },
        { 40f, "Medium" },
        { 30f, "High" },
        { 20f, "Very High" }
    };
    private float[] zooms = { 60f, 40f, 30f, 20f };
    private int zoomIndex;
    private int selectedIndex;

    public Button zoomButton;
    public Button closeButton;
    public CharacterMovement characterScript;
    public CameraOperations cameraScript;
    public Camera mainCamera;
    public GameObject binocularStand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Vertical");

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

        if (Input.GetButtonDown("js1") || Input.GetKeyDown(KeyCode.B))
        {
            if (selectedIndex == 0)
            {
                zoomIndex++;
                zoomIndex = zoomIndex % zooms.Length;
                UpdateZoom();
            }
            else
            {
                CloseOverlay();
            }

        }
    }

    void OnEnable()
    {
        zoomIndex = 0;
        selectedIndex = 0;
        UpdateButtonHighlight();
        UpdateZoom();
        binocularStand.SetActive(false);
        characterScript.enabled = false;
        cameraScript.enabled = false;
    }

    void OnDisable()
    {
        binocularStand.SetActive(true);
        characterScript.enabled = true;
        cameraScript.enabled = true;
        // reset zoom
        mainCamera.fieldOfView = 60f;
    }

    void UpdateZoom()
    {
        mainCamera.fieldOfView = zooms[zoomIndex];
        zoomButton.GetComponentInChildren<TMP_Text>().text = "Zoom: " + zoomMappings[zooms[zoomIndex]];
    }

    void CloseOverlay()
    {
        gameObject.SetActive(false);
    }


    void UpdateButtonHighlight()
    {
        ColorBlock selected = ColorBlock.defaultColorBlock;
        selected.normalColor = Color.yellow;

        ColorBlock normal = ColorBlock.defaultColorBlock;
        normal.normalColor = Color.white;

        zoomButton.colors = (selectedIndex == 0) ? selected : normal;
        closeButton.colors = (selectedIndex == 1) ? selected : normal;
    }
}
