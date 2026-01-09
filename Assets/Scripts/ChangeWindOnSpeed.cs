using UnityEngine;

public class ChangeWindOnSpeed : MonoBehaviour
{
    public ParticleSystem wind;
    public GameObject player;
    public Rigidbody playerRb;
    public AudioSource windSound;

    private void Start()
    {
        wind = GetComponent<ParticleSystem>();
        playerRb = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var emission = wind.emission;

        emission.rateOverTime = playerRb.linearVelocity.magnitude;

        windSound.volume = playerRb.linearVelocity.magnitude / 90;
    }
}