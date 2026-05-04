using UnityEngine;
using static FeedFoodLeaf;
using static AnimalBuffalo;
public class FoodLeaf : MonoBehaviour
{
    public GameObject curGameObject;
    public GameObject camGameObject;
    bool isPointerEnter = false;
    static bool feeding;
    static bool foodPickedUp = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPointerEnter)
        {
            gameObject.GetComponent<Outline>().enabled = true;
            if (Input.GetButtonDown("js1"))
            {
                // setFeedFood(true);
                AudioManager.Instance.PlayClick();
                foodPickedUp = true;
                foodLeafNum ++;
                camGameObject.SetActive(true);
                curGameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.GetComponent<Outline>().enabled = false;
        }
    }
    public static bool foodPickUp()
    {
        return foodPickedUp;
    }

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlayHighlight();
        isPointerEnter = true;
    }

    public void OnPointerExit()
    {
        AudioManager.Instance.PlayUnhighlight();
        isPointerEnter = false;
    }
}