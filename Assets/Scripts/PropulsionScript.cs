using UnityEngine;

public class PropulsionScript : MonoBehaviour
{
    public Rigidbody rb;
    public VisionRaycast vr;
    public PlayerScript player;
    public Camera mainCamera;
    public float chargeValue;

    public void Propulsion(Vector3 direction, float force)
    {
        if (direction == Vector3.zero)
            return;

        // Debug.Log("test propulsion");
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction * force, ForceMode.Impulse);
        vr.firstShot = Vector3.zero;
        vr.secondShot = Vector3.zero;
        player.lr_one.enabled = false;
        player.lr_two.enabled = false;
        player.state = States.noShot;
    }

    private void Start()
    {
        player = GetComponent<PlayerScript>();
        vr = mainCamera.GetComponent<VisionRaycast>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Debug.Log(rb.linearVelocity.magnitude);
    }
}