using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset = new Vector3(0, 1f, 0);
    public ParticleSystem impactEffect;
    public GameObject player;
    public Rigidbody playerRb;
    public PlayerScript playerScript;


    private void Start()
    {
        impactEffect = GetComponent<ParticleSystem>();
        playerRb = player.GetComponent<Rigidbody>();
        playerScript = player.GetComponent<PlayerScript>();
    }

    void Update()
    {
        var emission = impactEffect.emission;
        
        emission.rateOverTime = playerScript.velocity * 11;
        transform.position = playerTransform.position - offset;
    }
}
