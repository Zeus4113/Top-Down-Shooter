using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private Collider2D m_myCollider;
    private Vector3 m_position;
    private float m_movementSpeed;
	private float m_movementSpeedMultiplier;
    private float m_currentStamina;
    private bool m_isSprinting;
	private float m_currentScore;

    [SerializeField] private float m_maxStamina;
    [SerializeField] private float m_sprintSpeed;
    [SerializeField] private float m_walkSpeed;
	[SerializeField] private HealthStatsSO m_healthStats;

    public delegate void UpdateStamina(float stamina);
    public static UpdateStamina myStaminaChange;

    public void Init()
    {
        m_isSprinting = false;
		m_currentScore = 0;
		m_movementSpeedMultiplier = 1f;
		m_currentStamina = m_maxStamina;
        m_movementSpeed = m_walkSpeed;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_myCollider = GetComponent<Collider2D>();
        m_position = m_rigidbody.position;
        gameObject.GetComponentInChildren<Health>().Init(m_healthStats);
		ScoreParticle.OnParticlePickup += SetScore;
		ScoreDepot.depositTick += SetScore;
	}

    public void Run()
    {
        gameObject.GetComponentInChildren<Health>().Run();
        Movement();
        Sprint();
    }

	public float GetScore()
	{
		return m_currentScore;
	}

	public void SetScore(float amount)
	{
		m_currentScore += amount;
	}

    private void Movement()
    {
        m_position.x = Input.GetAxisRaw("Horizontal") * (m_movementSpeed * m_movementSpeedMultiplier);
        m_position.y = Input.GetAxisRaw("Vertical") * (m_movementSpeed * m_movementSpeedMultiplier);
        m_rigidbody.velocity = m_position;
    }

    private void Sprint()
    {
        if(m_currentStamina > 0)
        {
            if (Input.GetButtonDown("Sprint"))
            {
                m_movementSpeed = m_sprintSpeed;
                m_isSprinting = true;
                StartCoroutine(StaminaChange());


            }
            else if (Input.GetButtonUp("Sprint"))
            {
                m_movementSpeed = m_walkSpeed;
                m_isSprinting = false;

            }
        }
    }

    private IEnumerator StaminaChange()
    {
        while (m_isSprinting)
        {
            m_currentStamina--;
            myStaminaChange?.Invoke(m_currentStamina);
            yield return new WaitForSeconds(0.025f);

            if(m_currentStamina <= 0)
            {
                m_movementSpeed = m_walkSpeed;
                m_isSprinting = false;
                break;
            }
        }

        while (!m_isSprinting)
        {
            m_currentStamina++;
            myStaminaChange?.Invoke(m_currentStamina);
            yield return new WaitForSeconds(0.04f);

            if(m_currentStamina >= m_maxStamina)
            {
                break;
            }
        }

        yield return null;

    }

    public float GetMaxStamina()
    {
        return m_maxStamina;
    }
	
	public void SetSpeedMultiplier(float newMultiplier)
	{
		m_movementSpeedMultiplier = newMultiplier;
	}
}

