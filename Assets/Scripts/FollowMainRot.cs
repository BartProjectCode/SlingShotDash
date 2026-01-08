using UnityEngine;

public class FollowMainRot : MonoBehaviour
{
    public GameObject objectCamera;
    
    void Update()
    {
        transform.rotation = objectCamera.transform.rotation;
    }
}
