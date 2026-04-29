using UnityEngine;
using static FoodLeaf;
using static AnimalBuffalo;

public class FeedFoodLeaf : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public GameObject menu;
    static bool feeding;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("Buffalo") && foodPickUp())
            {
                if (Input.GetButtonDown("js1"))
                {
                    feeding = true;
                    foodLeafNum --;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public static bool getFeedFoodL()
    {
        return feeding;
    }
}
