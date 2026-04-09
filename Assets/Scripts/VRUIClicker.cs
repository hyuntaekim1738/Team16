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
        {
            ClickCenteredUIButton();
        }
    }

    bool IsBPressed()
    {
        return Input.GetKeyDown(KeyCode.K) ||
               Input.GetKeyDown(KeyCode.JoystickButton5);
    }

    void ClickCenteredUIButton()
    {
        if (EventSystem.current == null || uiCamera == null)
            return;

        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = new Vector2(Screen.width / 2f, Screen.height / 2f);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            Button button = result.gameObject.GetComponent<Button>();

            if (button == null)
            {
                button = result.gameObject.GetComponentInParent<Button>();
            }

            if (button != null && button.interactable)
            {
                button.onClick.Invoke();
                return;
            }

            TMP_InputField inputField = result.gameObject.GetComponent<TMP_InputField>();

            if (inputField == null)
            {
                inputField = result.gameObject.GetComponentInParent<TMP_InputField>();
            }

            if (inputField != null)
            {
                inputField.ActivateInputField();
                return;
            }
        }
    }
}