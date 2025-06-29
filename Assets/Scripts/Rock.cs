using System;
using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    CinemachineImpulseSource shakeSource;
    [SerializeField] ParticleSystem collisionSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        shakeSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayScreenShake();
        PlayCollisionFx(collision);
    }

    private void PlayCollisionFx(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        collisionSystem.transform.position = contact.point;

        collisionSystem.Play();
    }

    private void PlayScreenShake()
    {
        float disntance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = 1 / disntance;
        shakeIntensity = Mathf.Min(1, shakeIntensity);
        shakeSource.GenerateImpulse(shakeIntensity);
    }
}
