using UnityEngine;

public class TimePad : MonoBehaviour
{
    private float detectionRadius = .5f;
    private bool popupDisplayed = false;

    public bool isCurrentEra;
    public CharacterController character;
    public GameObject timeTravelConfirmationMessage;
    public string era;
    public string targetScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isCurrentEra)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.orange;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrentEra) return;
        Vector3 padPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPos = new Vector3(character.transform.position.x, 0, character.transform.position.z);
        float dist = Vector3.Distance(padPos, playerPos);

        bool playerOnPad = dist < detectionRadius;
        // if player walks on pad, display confirmation message that they want to time travel
        if (playerOnPad)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.orange;
            if (!popupDisplayed)
            {
                popupDisplayed = true;
                timeTravelConfirmationMessage.SetActive(true);
                timeTravelConfirmationMessage.GetComponent<TimeTravelConfirmationMessage>().Setup(era, targetScene);
            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            if (popupDisplayed)
            {
                timeTravelConfirmationMessage.SetActive(false);
                popupDisplayed = false;
            }
        }
    }

}
