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

    private void Start()
    {
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
    }

}
