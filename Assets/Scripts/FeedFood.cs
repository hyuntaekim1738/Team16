using UnityEngine;
using static Food;
using static Animal;
public class FeedFood : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public GameObject menu;
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
            if (hit.collider.tag.Equals("Wolf") && foodPickUp())
            {
                if (Input.GetButtonDown("js1"))
                {
                    feeding = true;
                    // menu.SetActive(false);
                    foodNum --;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public static bool getFeedFood()
    {
        return feeding;
    }

    public static void setFeedFood(bool b)
    {
        feeding = b;
    }
}
