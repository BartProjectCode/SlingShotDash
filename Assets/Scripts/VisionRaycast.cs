using UnityEngine;
using UnityEngine.UI;

public class VisionRaycast : MonoBehaviour
{
    private Camera camera1;
    [SerializeField] private Image crossair;
    [SerializeField] private float distance = 100f;
    public GameObject sphere;
    public Vector3 stringDir;
    public Transform player;

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
        
        Shoot();
    }

    public Vector3 Shoot()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, distance))
            {
                // Debug.Log(hit.transform.name);
                // Debug.Log(hit.point);
                Instantiate(sphere, hit.point, transform.rotation);
                stringDir = player.position - hit.point;
                Debug.Log(stringDir);
            }
        }

        return default;
    }
    
}
