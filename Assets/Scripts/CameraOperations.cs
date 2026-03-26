using UnityEngine;

public class CameraOperations : MonoBehaviour
{
    public CharacterController controller;
    public GameObject menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //menu open
        if (Input.GetButtonDown("js11") || Input.GetKeyDown(KeyCode.M))
        {
            menu.SetActive(true);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            //if the pointer points at the ground
            if (hit.collider.CompareTag("Ground"))
            {
                if ((Input.GetButtonDown("js0") || Input.GetKeyDown(KeyCode.Y)))
                {
                    Teleport(hit.point);
                }
            }
        }
    }

    void Teleport(Vector3 hitPoint)
    {
        Vector3 targetPosition = hitPoint;
        float radius = controller.radius;
        float height = controller.height;

        targetPosition.y += height * .5f; //added to prevent ground cliping

        //before teleporting, check that you won't collide with another object
        Vector3 bottom = targetPosition;
        Vector3 top = targetPosition + Vector3.up * height;
        if (!Physics.CheckCapsule(bottom, top, radius))
        {
            controller.enabled = false;
            controller.transform.position = targetPosition;
            controller.enabled = true;
        }
    }
}
