using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset = new Vector3(0, 1f, 0);

    void Update()
    {
        transform.position = playerTransform.position - offset;
    }
}
