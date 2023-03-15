using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private Collider2D m_myCollider;
    private Vector3 m_position;
    private float m_movementSpeed;
    private float m_currentStamina;
    private bool m_isSprinting;

    [SerializeField] private float m_maxStamina;
    [SerializeField] private float m_sprintSpeed;
    [SerializeField] private float m_walkSpeed;

    public delegate void UpdateStamina(float stamina);
    public static UpdateStamina myStaminaChange;

    public void Init()
    {
        m_isSprinting = false;
        m_currentStamina = m_maxStamina;
        m_movementSpeed = m_walkSpeed;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_myCollider = GetComponent<Collider2D>();
        m_position = m_rigidbody.position;
        gameObject.GetComponentInChildren<Health>().Init();
    }

    public void Run()
    {
        gameObject.GetComponentInChildren<Health>().Run();
        Movement();
        Sprint();
    }

    private void Movement()
    {
        m_position.x = Input.GetAxisRaw("Horizontal") * m_movementSpeed;
        m_position.y = Input.GetAxisRaw("Vertical") * m_movementSpeed;
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
}

