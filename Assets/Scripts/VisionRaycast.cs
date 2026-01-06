using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class VisionRaycast : MonoBehaviour
{
    private Camera camera1;
    [SerializeField] private Image crossair;
    [SerializeField] private float distance = 100f;
    public GameObject sphere;
    public Transform player;
    public GameObject allPlayer;
    public Rigidbody playerRb;

    public Vector3 stringDir;

    public Vector3 firstShot;
    public Vector3 secondShot;

    public AnimationCurve curve;
    public float duration = 5f;

    private float timer = 0f;
    public float force = 0f;

    private CinemachineCamera cineCam;
    public GameObject cineCamObject;

    private PlayerScript playerScript;

    private float maxFOV;

    public bool onGround;
    public float groundRayDistance;

    //variable pour le timer du FOV à l'atterissage au sol
    private float t = 0;
    

    private void Start()
    {
        playerRb = allPlayer.GetComponent<Rigidbody>();
        cineCam = cineCamObject.GetComponent<CinemachineCamera>();
        playerScript = player.GetComponent<PlayerScript>();
        Cursor.lockState = CursorLockMode.Locked;
        camera1 = Camera.main;
        maxFOV = 60f;
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

        if (!onGround)
        {
            float fov;
            fov = Mathf.Clamp((maxFOV + (-25f + playerRb.linearVelocity.magnitude / 2)), 60f, 1000f);
            cineCam.Lens.FieldOfView = fov;
        }

        Shoot();

        Debug.DrawRay(transform.position, -Vector3.up * groundRayDistance, Color.magenta);

        //Check si le joueur est sur le terrain
        Ray groundRay = new Ray(transform.position, -Vector3.up);
        RaycastHit groundHit;

        if (Physics.Raycast(groundRay, out groundHit, groundRayDistance))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        if (onGround && force == 0 && playerRb.linearVelocity.y == 0)
        {

            t += Time.deltaTime/2;
            
            if (t > 1f)
            {
                t = 1f;
            }

            Mathf.Lerp(cineCam.Lens.FieldOfView, 60f, t);
            Debug.Log(t);
            // cineCam.Lens.FieldOfView = 60f;
        }
        else
        {
            t = 0;
        }
        
    }

    public Vector3 Shoot()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (onGround && playerScript.state == States.noShot && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, distance))
        {
            Instantiate(sphere, hit.point, transform.rotation);
            stringDir = hit.point;
            firstShot = stringDir;
            Debug.Log("first shot value = " + firstShot + " second shot value = " + secondShot);
            // Debug.Log(stringDir);
            player.GetComponent<PlayerScript>().DrawLine();
        }
        else if (onGround && playerScript.state == States.oneShot && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, distance))
        {
            Instantiate(sphere, hit.point, transform.rotation);
            stringDir = hit.point;
            secondShot = stringDir;
            Debug.Log("first shot value = " + firstShot + " second shot value = " + secondShot);
            // Debug.Log(stringDir);
            player.GetComponent<PlayerScript>().DrawLine();
        }
        else if (playerScript.state == States.twoShot && Input.GetKey(KeyCode.Space))
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            force = curve.Evaluate(t) * 100f;
            maxFOV = cineCam.Lens.FieldOfView = 60 + force / 5;
            // Debug.Log(force);
        }
        else if (playerScript.state == States.twoShot && Input.GetKeyUp(KeyCode.Space))
        {
            player.GetComponent<PlayerScript>().DrawLine();
            timer = 0f;
            force = 0f;
            // cineCam.Lens.FieldOfView = 60;
        }

        if (Input.GetMouseButtonDown(1) && playerScript.state != States.noShot)
        {
            switch (playerScript.state)
            {
                case States.oneShot:
                    playerScript.lr_one.enabled = false;
                    firstShot = Vector3.zero;
                    break;

                case States.twoShot:
                    playerScript.lr_two.enabled = false;
                    secondShot = Vector3.zero;
                    break;
            }
            playerScript.state -= 1;
        }

        return default;
    }
}