using System.Runtime.CompilerServices;
using UnityEngine;

public enum States
{
    noShot,
    oneShot,
    twoShot
}

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    public LineRenderer lr_one;
    public LineRenderer lr_two;
    public States state;
    public Vector3 dir;
    public Camera mainCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Propulsion(Vector3 direction, float force)
    {
        if (direction == Vector3.zero)
            return;
    }

    public void DrawLine()
    {
        Debug.Log(state);
        dir = mainCamera.GetComponent<VisionRaycast>().stringDir;
        switch (state)
        {
            case States.noShot:
                lr_one.enabled = true;
                lr_one.SetPosition(0, transform.position);
                lr_one.SetPosition(1, dir);
                state = States.oneShot;
                break;

            case States.oneShot:
                lr_two.enabled = true;
                lr_two.SetPosition(0, transform.position);
                lr_two.SetPosition(1, dir);
                state = States.twoShot;
                break;

            case States.twoShot:
                break;
        }
    }

    private void Start()
    {
        lr_one.positionCount = 2;
        lr_two.positionCount = 2;
        state = States.noShot;
    }

    // Update is called once per frame
    private void Update()
    {
        //lr_one.SetPosition(0, transform.position);
        //lr_one.SetPosition(1, new Vector3(1, 1, 1));
    }
}