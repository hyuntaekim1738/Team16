using UnityEngine;
using static ExitMenuButton;
using static FeedFood;
using static Food;
using static FeedFoodLeaf;


public class Animal : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public GameObject menu;
    public GameObject character;
    bool isPointerEnter = false;
    bool isMenuOpen = false;
    bool feeding = false;
    bool feedingDone, feedingDoneL = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        feeding = foodPickUp();
        feedingDone = getFeedFood();
        feedingDoneL = getFeedFoodL();
        // ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        // if(Physics.Raycast(ray, out hit))
        // {
            if (isPointerEnter)
            {
                gameObject.GetComponent<Outline>().enabled = true;
                if (Input.GetButton("js5"))
                {
                    if(!feeding){
                        Debug.Log("hi ");
                        menu.SetActive(true);
                        isMenuOpen = true;
                        character.GetComponent<CharacterMovement>().enabled = false;
                    }
                    else if (feedingDone || feedingDoneL)
                    {
                        Debug.Log("hi done");
                        if (Input.GetButtonDown("js5"))
                        {
                            menu.SetActive(true);
                            isMenuOpen = true;
                            character.GetComponent<CharacterMovement>().enabled = false;
                        }
                    }
                }
            }
            else
            {
                gameObject.GetComponent<Outline>().enabled = false;
            }
        
        if (animalMenuClosed())
        {
            isMenuOpen = false;
        }
        // feeding = getFeedFood();
        // }
        // else
        // {
        //     gameObject.GetComponent<Outline>().enabled = false;
        // }
        // }
    }

    public void OnPointerEnter()
    {
        if(!isMenuOpen)
            isPointerEnter = true;
        // if (Input.GetButton("js5"))
        //     menu.SetActive(true);
    }

    public void OnPointerExit()
    {
        isPointerEnter = false;
    }
}
