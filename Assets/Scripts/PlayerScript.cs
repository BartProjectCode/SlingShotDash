using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Splines.Interpolators;

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
    public float startWidth = 0.25f;
    public float widthFactor = 10f;
    public States state;
    public Vector3 dir;
    public Camera mainCamera;
    public Vector3 midDirection;
    public Vector3 dashDirection;
    public VisionRaycast vr;
    public PropulsionScript pr;
    public float force;
    public Material mat_lr;
    public Color colorMin = Color.green;
    public Color colorMid = Color.green;
    public Color colorMax = Color.red;

    public bool multiplyGrav;

    public float dashStrength;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        vr = mainCamera.GetComponent<VisionRaycast>();
        pr = GetComponent<PropulsionScript>();
    }

    public void DrawLine()
    {
        Debug.Log(state);
        dir = mainCamera.GetComponent<VisionRaycast>().stringDir;
        switch (state)
        {
            case States.noShot:
                lr_one.enabled = true;
                lr_one.SetPosition(1, vr.firstShot);
                state = States.oneShot;
                break;

            case States.oneShot:
                lr_two.enabled = true;
                lr_two.SetPosition(1, vr.secondShot);
                state = States.twoShot;
                break;

            case States.twoShot:
                midDirection = (vr.firstShot + vr.secondShot) / 2f;
                dashDirection = (midDirection - transform.position).normalized;
                pr.Propulsion(dashDirection, vr.force * dashStrength);
                break;
        }
    }

    private Color GetChargeColor(float charge)
    {
        charge = Mathf.Clamp01(charge);

        if (charge <= 0.33f)
        {
            float t = charge / 0.33f;
            return Color.Lerp(Color.green, Color.yellow, t);
        }
        else if (charge <= 0.667f)
        {
            float t = (charge - 0.33f) / (0.667f - 0.33f);
            return Color.Lerp(Color.yellow, new Color(1f, 0.5f, 0f), t); // orange
        }
        else
        {
            float t = (charge - 0.667f) / (1f - 0.667f);
            return Color.Lerp(new Color(1f, 0.5f, 0f), Color.red, t);
        }
    }

    public void UpdateColor(float chargeValue)
    {
        if (chargeValue == 0)
        {
            mat_lr.color = Color.white;
        }
        else
        {
            mat_lr.color = GetChargeColor(chargeValue);
        }
    }

    private void Start()
    {
        lr_one.positionCount = 2;
        lr_two.positionCount = 2;
        lr_one.startWidth = startWidth;
        lr_two.startWidth = startWidth;
        state = States.noShot;
    }

    // Update is called once per frame
    private void Update()
    {
        lr_one.SetPosition(0, transform.position);
        lr_two.SetPosition(0, transform.position);
        UpdateColor((vr.force) / 100);
        //lr_one.SetPosition(0, transform.position);
        //lr_one.SetPosition(1, new Vector3(1, 1, 1));

        if (!vr.onGround && rb.linearVelocity.y < 10 && multiplyGrav)
        {
            multiplyGrav = false;
            Physics.gravity = new Vector3(0f, -60f, 0f);
        }
        else if (vr.onGround)
        {
            multiplyGrav = true;
            Physics.gravity = new Vector3(0f, -29.43f, 0f);
        }
    }
}