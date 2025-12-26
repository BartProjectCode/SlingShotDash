using UnityEngine;

public class PropulsionScript : MonoBehaviour
{
    public Rigidbody rb;

    public void Propulsion(Vector3 direction, float force)
    {
        if (direction == Vector3.zero)
            return;

        Debug.Log("test propulsion");
        direction = direction.normalized;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}