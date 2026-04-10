using UnityEngine;
using static ExitMenuButton;
using static FeedFood;
using static Food;
using static FeedFoodLeaf;
public class AnimalBuffalo : MonoBehaviour
{
    public GameObject menu;
    public GameObject character;
    public GameObject camera;
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
        if (isPointerEnter)
            {
                gameObject.GetComponent<Outline>().enabled = true;
                if (Input.GetButton("js1"))
                {   
                    if(!feeding){
                        // Debug.Log("menu open");
                        menu.SetActive(true);
                        isMenuOpen = true;
                        character.GetComponent<CharacterMovement>().enabled = false;
                        camera.GetComponent<CameraOperations>().enabled = false;
                    }
                    else if (feedingDoneL)
                    {
                        // Debug.Log("leaf done");
                        feeding = foodPickUp();
                            if (Input.GetButtonDown("js1"))
                            {
                                menu.SetActive(true);
                                isMenuOpen = true;
                                character.GetComponent<CharacterMovement>().enabled = false;
                                camera.GetComponent<CameraOperations>().enabled = false;
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
    }
    public void OnPointerEnter()
    {
        if(!isMenuOpen)
            isPointerEnter = true;
    }

    public void OnPointerExit()
    {
        isPointerEnter = false;
    }
}
