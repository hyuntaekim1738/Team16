using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

public class RangerMenuUI : MonoBehaviour
{
    private List<ConversationTurn> conversationHistory = new List<ConversationTurn>();

    [Header("Main UI References")]
    public GameObject menuRoot;
    public GameObject mainMenuPanel;
    public TextMeshProUGUI responseText;

    [Header("Custom Question UI")]
    public GameObject customQuestionPanel;
    public TMP_InputField questionInput;

    [Header("Disable Gameplay While Menu Is Open")]
    public CharacterMovement characterScript;
    public CameraOperations cameraScript;

    [Header("Text Content")]
    [TextArea(2, 4)]
    public string defaultMessage = "Ask me about this era of Yellowstone.";

    [TextArea(2, 4)]
    public string eraName = "Founding Era (1872 - 1886)";

    [TextArea(3, 8)]
    public string historyResponse;

    [TextArea(3, 8)]
    public string wildlifeResponse;

    [TextArea(3, 8)]
    public string buildingsResponse;

    private string rangerApiUrl = "http://IP-HERE:3000/ask-ranger";

    void OnEnable()
    {
        if (responseText != null)
        {
            responseText.text = defaultMessage;
        }

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        if (customQuestionPanel != null)
        {
            customQuestionPanel.SetActive(false);
        }

        if (questionInput != null)
        {
            questionInput.text = "";
        }

        if (characterScript != null)
        {
            characterScript.enabled = false;
        }

        if (cameraScript != null)
        {
            cameraScript.enabled = false;
        }
    }



    public void ShowHistory()
    {
        if (responseText != null)
        {
            responseText.text = historyResponse;
        }
    }

    public void ShowWildlife()
    {
        if (responseText != null)
        {
            responseText.text = wildlifeResponse;
        }
    }

    public void ShowBuildings()
    {
        if (responseText != null)
        {
            responseText.text = buildingsResponse;
        }
    }

    public void OpenCustomQuestionPanel()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }

        if (customQuestionPanel != null)
        {
            customQuestionPanel.SetActive(true);
        }

        if (questionInput != null)
        {
            questionInput.text = "";
        }
    }

    public void CancelCustomQuestion()
    {
        if (customQuestionPanel != null)
        {
            customQuestionPanel.SetActive(false);
        }

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        if (questionInput != null)
        {
            questionInput.text = "";
        }
    }

    public void SubmitCustomQuestion()
    {
        if (questionInput == null || responseText == null)
            return;

        string userQuestion = questionInput.text.Trim();

        if (string.IsNullOrEmpty(userQuestion))
        {
            responseText.text = "Please speak or enter a question first.";
            return;
        }

        StartCoroutine(SendQuestionToRanger(userQuestion));
    }
    
    private IEnumerator SendQuestionToRanger(string userQuestion)
    {
        responseText.text = "Ranger is thinking...";

        if (customQuestionPanel != null)
        {
            customQuestionPanel.SetActive(false);
        }

        RangerRequestBody body = new RangerRequestBody
        {
            eraName = eraName,
            question = userQuestion,
            history = conversationHistory.TakeLast(10).ToArray()
        };

        string json = JsonUtility.ToJson(body);

        using (UnityWebRequest request = new UnityWebRequest(rangerApiUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseText.text = "Sorry, I couldn't reach the ranger AI right now.";
            }
            else
            {
                string responseJson = request.downloadHandler.text;
                RangerResponseBody response = JsonUtility.FromJson<RangerResponseBody>(responseJson);

                if (response != null && response.ok)
                {
                    responseText.text = response.answer;
                    // remember the exchange
                    conversationHistory.Add(new ConversationTurn {role = "user", text = userQuestion});
                    conversationHistory.Add(new ConversationTurn {role = "ranger", text = response.answer});
                }
                else
                {
                    responseText.text = "Sorry, I couldn't get a valid ranger response.";
                }
            }
        }

        if (questionInput != null)
        {
            questionInput.text = "";
        }

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
    }

    public void CloseMenu()
    {
        if (customQuestionPanel != null)
        {
            customQuestionPanel.SetActive(false);
        }

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        if (questionInput != null)
        {
            questionInput.text = "";
        }

        if (characterScript != null)
        {
            characterScript.enabled = true;
        }

        if (cameraScript != null)
        {
            cameraScript.enabled = true;
        }

        if (menuRoot != null)
        {
            menuRoot.SetActive(false);
        }
    }

    public void SubmitSpeechQuestion(string spokenText)
    {
        if (questionInput == null || responseText == null)
            return;

        if (string.IsNullOrWhiteSpace(spokenText))
        {
            responseText.text = "I didn't catch that. Please try again.";
            return;
        }

        questionInput.text = spokenText.Trim();
        SubmitCustomQuestion();
    }

}

[System.Serializable]
public class ConversationTurn
{
    public string role;//user or ranger
    public string text;
}

[System.Serializable]
public class RangerRequestBody
{
    public string eraName;
    public string question;
    public ConversationTurn[] history;
}

[System.Serializable]
public class RangerResponseBody
{
    public bool ok;
    public string answer;
}