using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class VRUIClicker : MonoBehaviour
{
    public Camera uiCamera;

    void Update()
    {
        if (IsBPressed())
            ClickCenteredUIButton();
    }

    bool IsBPressed()
    {
        return Input.GetKeyDown(KeyCode.K) ||
               Input.GetButtonDown("js1");
    }

    void ClickCenteredUIButton()
    {
        Debug.Log("CLICKING");
        Ray ray = uiCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 100f))
            return;
        Debug.Log("HIT");
        Button button = hit.collider.GetComponent<Button>()
                     ?? hit.collider.GetComponentInParent<Button>();
        if (button != null && button.interactable)
        {
            button.onClick.Invoke();
            return;
        }

        TMP_InputField inputField = hit.collider.GetComponent<TMP_InputField>()
                                 ?? hit.collider.GetComponentInParent<TMP_InputField>();
        if (inputField != null)
        {
            inputField.ActivateInputField();
        }
    }
}