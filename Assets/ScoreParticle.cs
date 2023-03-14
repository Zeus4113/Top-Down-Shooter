using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreParticle : MonoBehaviour
{
    [SerializeField] private int m_scorePerParticle;

    private ParticleSystem m_mySystem;
    private List<ParticleSystem.Particle> m_particles;
    private GameObject m_playerRef;
    private ParticleSystem.TriggerModule m_myTrigger;

    public delegate void ParticlePickup(int amount);
    public static ParticlePickup OnParticlePickup;

    private void Start()
    {
        m_particles = new List<ParticleSystem.Particle>();
        m_mySystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {

        int particleCount = ParticlePhysicsExtensions.GetTriggerParticles(m_mySystem, ParticleSystemTriggerEventType.Enter, m_particles);

        for(int i = 0; i < particleCount; i++)
        {
            OnParticlePickup?.Invoke(m_scorePerParticle);
        }

        m_mySystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, m_particles);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == null) return;

        GameObject myObject = collision.gameObject;

        if (myObject.CompareTag("Player"))
        {
            m_playerRef = myObject;
            m_myTrigger = m_mySystem.trigger;

            if(m_myTrigger.GetCollider(0) == null)
            {
                m_myTrigger.SetCollider(0, m_playerRef.GetComponent<Collider2D>());
            }          
        }
    }
}
