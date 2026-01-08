using System;
using System.Collections;
using UnityEngine;

public class PCameraShakeController : MonoBehaviour
{
    public Vector3 startPosition;
    public Transform startEyePosition;
    public float TimeUsed = 0f;

    public GameObject visionCameraObject;
    public VisionRaycast vr;
    public float strength;
    public GameObject player;
    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
        vr = visionCameraObject.GetComponent<VisionRaycast>();
    }


    private void Update()
    {
        startPosition = transform.position;
        
        if (vr.force >= 100)
        {
            strength += Time.deltaTime;
            
            transform.position = startEyePosition.position + UnityEngine.Random.insideUnitSphere * (strength / 20);
        }
        else
        {
            strength = 0;
            transform.position = startEyePosition.position;
        }

        if (vr.onGround == false)
        {
            transform.position = startEyePosition.position + UnityEngine.Random.insideUnitSphere * ( playerRb.linearVelocity.magnitude / 2000);
        }


    }

}
