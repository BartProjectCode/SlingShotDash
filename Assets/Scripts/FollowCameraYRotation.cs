using UnityEngine;

public class FollowCameraYRotation : MonoBehaviour
{
    [SerializeField] private Transform cam;
    
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, cam.rotation.eulerAngles.y, 0);
        // transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * (Time.deltaTime));
        // transform.rotation = cam.rotation;
    }
}
