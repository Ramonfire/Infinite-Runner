using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    CinemachineImpulseSource shakeSource;
    AudioSource collisionSound;
    [SerializeField] ParticleSystem collisionSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        shakeSource = GetComponent<CinemachineImpulseSource>();
        collisionSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayScreenShake();
        PlayCollisionVFx(collision);
        PlayCollisionSound();
    }

    private void PlayCollisionSound()
    {
        if (collisionSound!=null)
        {
            if (collisionSound.isPlaying)
                collisionSound.Stop();
            float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            float volume = 1 / distance;
            volume = Mathf.Min(0.5f, volume);
            collisionSound.volume = volume;
            collisionSound.Play();
        }
    }

    private void PlayCollisionVFx(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        collisionSystem.transform.position = contact.point;

        collisionSystem.Play();
    }

    private void PlayScreenShake()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = 1 / distance;
        shakeIntensity = Mathf.Min(1, shakeIntensity);
        shakeSource.GenerateImpulse(shakeIntensity);
    }
}
