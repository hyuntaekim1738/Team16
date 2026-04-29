using TMPro;
using UnityEngine;

public class SpeechToTextManager : MonoBehaviour, ISpeechToTextListener
{
    [Header("References")]
    public RangerMenuUI rangerMenuUI;
    public TMP_InputField questionInput;
    public TextMeshProUGUI responseText;
    public ParkRangerAnimationController animationController;

    [Header("Settings")]
    public string languageCode = "en-US";
    public bool autoSubmitAfterSpeech = true;
    public bool preferOfflineRecognition = false;

    private bool isInitialized = false;

    private void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS
        isInitialized = SpeechToText.Initialize(languageCode);

        if (!isInitialized)
        {
            Debug.LogWarning("SpeechToText could not initialize with language: " + languageCode);
        }
#endif
    }

    public void StartListening()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (!isInitialized)
        {
            isInitialized = SpeechToText.Initialize(languageCode);

            if (!isInitialized)
            {
                if (responseText != null)
                    responseText.text = "Speech recognition could not initialize.";
                return;
            }
        }

        if (!SpeechToText.IsServiceAvailable(preferOfflineRecognition))
        {
            if (responseText != null)
                responseText.text = "Speech recognition service is unavailable on this device.";
            return;
        }

        if (SpeechToText.IsBusy())
        {
            if (responseText != null)
                responseText.text = "Speech recognition is already running.";
            return;
        }

        if (responseText != null)
            responseText.text = "Requesting microphone permission...";

        SpeechToText.RequestPermissionAsync((permission) =>
        {
            if (permission == SpeechToText.Permission.Granted)
            {
                bool started = SpeechToText.Start(
                    this,
                    useFreeFormLanguageModel: true,
                    preferOfflineRecognition: preferOfflineRecognition
                );

                if (started)
                {
                    if (responseText != null)
                        responseText.text = "Listening...";
                }
                else
                {
                    if (responseText != null)
                        responseText.text = "Couldn't start speech recognition.";
                }
            }
            else
            {
                if (responseText != null)
                    responseText.text = "Microphone permission denied.";
            }
        });
#else
        if (responseText != null)
            responseText.text = "Speech-to-text works on Android/iOS device builds only.";
#endif
    }

    public void StopListening()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (SpeechToText.IsBusy())
        {
            SpeechToText.ForceStop();

            if (responseText != null)
                responseText.text = "Stopping...";
        }
#endif
    }

    public void OnReadyForSpeech()
    {
        Debug.Log("SpeechToText: Ready for speech");
    }

    public void OnBeginningOfSpeech()
    {
        Debug.Log("SpeechToText: Beginning of speech");

        if (responseText != null)
            responseText.text = "Listening...";
    }

    public void OnVoiceLevelChanged(float normalizedVoiceLevel)
    {
        // Optional, ignore for now
    }

    public void OnPartialResultReceived(string spokenText)
    {
        if (questionInput != null)
            questionInput.text = spokenText;

        if (responseText != null)
            responseText.text = "Hearing: " + spokenText;
    }

    public void OnResultReceived(string spokenText, int? errorCode)
    {
        if (!string.IsNullOrWhiteSpace(spokenText))
        {
            if (questionInput != null)
                questionInput.text = spokenText.Trim();

            if (responseText != null)
                responseText.text = "You asked: " + spokenText.Trim();

            if (autoSubmitAfterSpeech && rangerMenuUI != null)
            {
                rangerMenuUI.SubmitSpeechQuestion(spokenText.Trim());
            }
            if (animationController != null)
            {
                animationController.PlayTalk();
            }
        }
        else
        {
            if (responseText != null)
            {
                if (errorCode.HasValue)
                    responseText.text = "I didn't catch that. Error code: " + errorCode.Value;
                else
                    responseText.text = "I didn't catch that. Please try again.";
            }
        }
    }
}