using UnityEngine;
public class AnimalMenu : MonoBehaviour
{
    Transform camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = camera.position + camera.forward * 2f + camera.up * 0.45f;
        transform.position = camera.position + new Vector3(camera.forward.x, 0, camera.forward.z).normalized * 2f;
        // transform.rotation = Camera.main.transform.rotation;
        transform.LookAt(transform.position + camera.forward, camera.up);
        // transform.LookAt(new Vector3(camera.position.x, transform.position.y, camera.position.z));
    }
}
