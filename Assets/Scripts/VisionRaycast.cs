using UnityEngine;
using UnityEngine.UI;

public class VisionRaycast : MonoBehaviour
{
    private Camera camera1;
    [SerializeField] private Image crossair;
    [SerializeField] private float distance = 100f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camera1 = Camera.main;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * distance, Color.blue);
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            crossair.color = Color.red;
        }
        else
        {
            crossair.color = Color.white;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, distance))
            {
                Debug.Log(hit.transform.name);
            }
        }
        
    }
}
