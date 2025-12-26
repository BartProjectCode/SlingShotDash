using UnityEngine;
using UnityEngine.UI;

public class VisionRaycast : MonoBehaviour
{
    private Camera camera1;
    [SerializeField] private Image crossair;
    [SerializeField] private float distance = 100f;
    public GameObject sphere;
    public Transform player;
    
    public Vector3 stringDir;

    public Vector3 firstShot;
    public Vector3 secondShot;

    private PlayerScript playerScript;
    

    private void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        Cursor.lockState = CursorLockMode.Locked;
        camera1 = Camera.main;
    }

    private void Update()
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

        if (playerScript.state == States.noShot && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, distance))
        {
            Instantiate(sphere, hit.point, transform.rotation);
            stringDir = hit.point;
            firstShot = stringDir;
            Debug.Log("first shot value = " + firstShot +  " second shot value = " + secondShot);
            // Debug.Log(stringDir);
            player.GetComponent<PlayerScript>().DrawLine();
        }
        else if (playerScript.state == States.oneShot && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, distance))
        {
            Instantiate(sphere, hit.point, transform.rotation);
            stringDir = hit.point;
            secondShot = stringDir;
            Debug.Log("first shot value = " + firstShot +  " second shot value = " + secondShot);
            // Debug.Log(stringDir);
            player.GetComponent<PlayerScript>().DrawLine();
        }

        if (Input.GetMouseButtonDown(1) && playerScript.state != States.noShot)
        {
            playerScript.state -= 1;
        }

        return default;
    }
}