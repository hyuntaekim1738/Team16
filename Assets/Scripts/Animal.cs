using UnityEngine;
using static ExitMenuButton;
using static FeedFood;
using static Food;
using static FeedFoodLeaf;
using static AnimalBuffalo;

public class Animal : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public GameObject menu;
    public GameObject character;
    public GameObject camera;
    bool isPointerEnter, feeding, feedingDone = false;
    public static bool isMenuOpen = false;
    public static int foodNum = 0;
    float dis;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector3.Distance(character.transform.position, transform.position);
        feeding = foodPickUp();
        feedingDone = getFeedFood();
        // feedingDoneL = getFeedFoodL();
            if (isPointerEnter)
            {
                if(isMenuOpenL || isMenuOpen) return;
                gameObject.GetComponent<Outline>().enabled = true;
                if (Input.GetButtonDown("js1"))
                {   
                    if(!feeding && foodLeafNum == 0){ // if there is no food in hand
                        OpenMenu();
                    }
                    else if (feedingDone && !isMenuOpen) // if feeding completed (to wolf)
                    {
                            // feeding = foodPickUp();
                            if (Input.GetButton("js1")) 
                            {
                                if(foodNum == 0 && foodLeafNum == 0){ // if there's no extra food
                                    OpenMenu();
                                    feedingDone = false;
                                }
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
    void OpenMenu(){
        menu.SetActive(true);
        isMenuOpen = true;
        character.GetComponent<CharacterMovement>().enabled = false;
        camera.GetComponent<CameraOperations>().enabled = false;
    }

    public void OnPointerEnter()
    {
        if(!isMenuOpen && !isMenuOpenL || animalMenuClosed())
            if(dis <= 7.5f)
                isPointerEnter = true;
    }

    public void OnPointerExit()
    {
        isPointerEnter = false;
    }
}
