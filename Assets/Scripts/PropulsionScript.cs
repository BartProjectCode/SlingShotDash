using UnityEngine;

public class PropulsionScript : MonoBehaviour
{
    public Rigidbody rb;
    public VisionRaycast vr;
    public PlayerScript player;
    public Camera mainCamera;
    public float chargeValue;
    public AudioSource propulsionSound;

    public void Propulsion(Vector3 direction, float force)
    {
        if (direction == Vector3.zero)
            return;

        // Debug.Log("test propulsion");
        rb.linearVelocity = Vector3.zero;
        propulsionSound.volume = force;
        rb.AddForce(direction * force, ForceMode.Impulse);
        vr.ResetLineOne();
        vr.ResetLineTwo();
        player.state = States.noShot;
        if (force > 15)
            propulsionSound.Play();
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