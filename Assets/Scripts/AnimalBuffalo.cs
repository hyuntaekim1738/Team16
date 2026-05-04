using UnityEngine;
using static ExitMenuButton;
using static FeedFood;
using static FoodLeaf;
using static FeedFoodLeaf;
using static Animal;
public class AnimalBuffalo : MonoBehaviour
{
    public GameObject menu;
    public GameObject character;
    public GameObject camera;
    bool isPointerEnter = false;
    public static bool isMenuOpenL = false;
    bool feeding = false;
    bool feedingDone, feedingDoneL = false;
    public static int foodLeafNum = 0;
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
        feedingDoneL = getFeedFoodL();
        if (isPointerEnter)
            {
                if(isMenuOpenL || isMenuOpen) return;
                gameObject.GetComponent<Outline>().enabled = true;
                if (Input.GetButton("js1"))
                {   
                    if(!feeding && foodNum == 0){ // there is no food picked up
                        OpenMenu();
                    }
                    else if (feedingDoneL)
                    {
                        feeding = foodPickUp();
                        if (Input.GetButtonDown("js1"))
                        {
                            if(foodLeafNum == 0 && foodNum == 0) //food is not remaining
                            {    
                                OpenMenu();
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
            isMenuOpenL = false;
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
        if(!isMenuOpenL && !isMenuOpen)
            if (dis <= 7.5f)
            {
                isPointerEnter = true;
                AudioManager.Instance.PlayHighlight();
            }
    }

    public void OnPointerExit()
    {
        if (gameObject.GetComponent<Outline>().enabled)
        {
            AudioManager.Instance.PlayUnhighlight();
        }
        isPointerEnter = false;
    }
}
