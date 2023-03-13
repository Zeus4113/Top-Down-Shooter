using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreParticle : MonoBehaviour
{
    [SerializeField] private int m_scorePerParticle;

    private ParticleSystem m_mySystem;

    private List<ParticleSystem.Particle> m_particles;

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
            ParticleSystem.Particle particle = m_particles[i];
            particle.startColor = Color.red;
            m_particles[i] = particle;
            OnParticlePickup?.Invoke(m_scorePerParticle);
        }

        m_mySystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, m_particles);
    }
}
