using UnityEngine;
using static FoodLeaf;

public class FeedFoodLeaf : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    // public GameObject menu;
    static bool feeding;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        feeding = false;
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
                    gameObject.SetActive(false);
                }
            }
            // else if (hit.collider.tag.Equals("Wolf") && foodPickUp())
            // {
            //     // Debug.Log("Wrong Animal");
            //     if (Input.GetButtonDown("js1"))
            //         feeding = false;
            // }
        }
    }

    public static bool getFeedFoodL()
    {
        return feeding;
    }
}
