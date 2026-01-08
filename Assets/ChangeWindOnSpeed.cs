using UnityEngine;

public class ChangeWindOnSpeed : MonoBehaviour
{
    public ParticleSystem wind;
    public GameObject player;
    public Rigidbody playerRb;
    
    void Start()
    {
        wind = GetComponent<ParticleSystem>();
        playerRb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        var emission = wind.emission;
        
        emission.rateOverTime = playerRb.linearVelocity.magnitude;
    }
}
