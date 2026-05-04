using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    void Update()
    {
        if (Camera.main == null) return;

        Vector3 dir = transform.position - Camera.main.transform.position;
        dir.y = 0; // keeps UI upright

        transform.rotation = Quaternion.LookRotation(dir);
    }
}