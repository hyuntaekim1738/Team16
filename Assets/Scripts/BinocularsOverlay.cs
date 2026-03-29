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
    private RenderTexture zoomRT;

    public Button zoomButton;
    public Button closeButton;
    public CharacterMovement characterScript;
    public CameraOperations cameraScript;
    public Camera mainCamera;
    public Camera zoomCamera;
    public GameObject binocularStand;
    public RawImage zoomDisplay;

    void Awake()
    {
        zoomRT = new RenderTexture(1024, 512, 24);
        zoomRT.Create();
    }

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
        zoomCamera.stereoTargetEye = StereoTargetEyeMask.None;
        zoomCamera.targetTexture = zoomRT;
        zoomDisplay.texture = zoomRT;
        zoomCamera.enabled = true;
        zoomDisplay.enabled = true;
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
        zoomCamera.enabled = false;
        zoomCamera.targetTexture = null;
        zoomDisplay.enabled = false;
        binocularStand.SetActive(true);
        characterScript.enabled = true;
        cameraScript.enabled = true;
        zoomCamera.enabled = false;
    }

    void UpdateZoom()
    {
        zoomCamera.fieldOfView = zooms[zoomIndex];
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
