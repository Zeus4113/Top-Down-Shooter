using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemy : MonoBehaviour, INavigable
{

    // Private Member Variables
    private bool m_attackReset;
    private float m_defaultSpeed;
    private state m_currentState;
    private Vector3 m_newPosition;
    private Vector3 m_currentPosition;
    private GameObject m_playerRef;
    private Health m_healthComponent;
    private Transform m_firePos;


    // Serialized Member Variables
    [SerializeField] private float m_attackDamage;
    [SerializeField] private float m_attackCooldown;
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private float m_chaseSpeedMultiplier;
    [SerializeField] private float m_shootAttackRange;
    [SerializeField] private float m_meleeAttackRange;
    [SerializeField] private GameObject m_myBullet;



    public void Start()
    {
        m_firePos = transform.GetChild(0);
        m_healthComponent = GetComponent<Health>();
        m_healthComponent.Init();
        m_currentState = state.wandering;
        m_newPosition = FindNewPatrolPosition();
        m_defaultSpeed = m_movementSpeed;
        m_attackReset = true;
    }

    public void Run()
    {

    }
    
    public void Update()
    {
        m_healthComponent.Run();

        // AI State Machine
        switch (m_currentState)
        {
            // Wanders the navigatable area until the player is detected
            case state.wandering:

                m_movementSpeed = m_defaultSpeed;
                m_currentPosition = transform.position;

                if (Vector3.Distance(m_newPosition, m_currentPosition) < 0.1f)
                {
                    m_newPosition = FindNewPatrolPosition();
                }
                else if (Vector3.Distance(m_newPosition, m_currentPosition) >= 0.1f)
                {
                    MoveToPosition(m_currentPosition, m_newPosition);
                }

                break;

            // Chases the player and attacks when in range
            case state.chasing:

                if (Vector3.Distance(transform.position, GetPlayerPosition(m_playerRef)) > m_meleeAttackRange)
                {
                    m_movementSpeed = m_defaultSpeed * m_chaseSpeedMultiplier;
                    MoveToPosition(transform.position, GetPlayerPosition(m_playerRef));
                }

                if (Vector3.Distance(transform.position, GetPlayerPosition(m_playerRef)) < m_shootAttackRange && Vector3.Distance(transform.position, GetPlayerPosition(m_playerRef)) > m_meleeAttackRange && m_attackReset)
                {
                    m_movementSpeed = m_defaultSpeed / (m_chaseSpeedMultiplier * 2);
                    RangeAttack(m_playerRef);
                }

                if (Vector3.Distance(transform.position, GetPlayerPosition(m_playerRef)) < m_meleeAttackRange && m_attackReset)
                {
                    m_movementSpeed = m_defaultSpeed / m_chaseSpeedMultiplier;
                    MeleeAttack(m_playerRef);
                }

                // Attack cooldown to ensure balance
                if (m_attackCooldown >= 0)
                {
                    m_attackCooldown -= Time.deltaTime;
                }
                else if (m_attackCooldown <= 0)
                {
                    m_attackCooldown = 0;
                    m_attackReset = true;
                }

                break;

        }
    }
    private void RangeAttack(GameObject myTarget)
    {
        Health targetHealth = myTarget.GetComponent<Health>();
        Rigidbody2D targetBody = myTarget.GetComponent<Rigidbody2D>();
        Debug.Log("Range Attacking!");

        RaycastHit2D myHit = Physics2D.Raycast(transform.position, myTarget.transform.position);

        if (myHit.collider == null) return;

        FaceEnemy(myTarget);
        GameObject myProjectile = Instantiate(m_myBullet, m_firePos.position, Quaternion.identity);
        Debug.Log("Spawned:", myProjectile); 
        myProjectile.GetComponent<Bullet>().Init();

        m_attackReset = false;
        m_attackCooldown = 1.5f;

        //targetBody.AddRelativeForce(transform.up * m_attackForce, ForceMode2D.Impulse);
        //m_rigidbody.AddRelativeForce(-transform.up * m_attackForce, ForceMode2D.Impulse);

    }

    private void MeleeAttack(GameObject myTarget)
    {
        Health targetHealth = myTarget.GetComponent<Health>();
        Rigidbody2D targetBody = myTarget.GetComponent<Rigidbody2D>();


        Debug.Log("Melee Attacking!");

        targetHealth.Damage(m_attackDamage);
        m_attackReset = false;
        m_attackCooldown = 1.5f;

        //targetBody.AddRelativeForce(transform.up * m_attackForce, ForceMode2D.Impulse);
        //m_rigidbody.AddRelativeForce(-transform.up * m_attackForce, ForceMode2D.Impulse);

    }



    public void SetState(state newState)
    {
        m_currentState = newState;
    }

    public state GetState()
    {
        return m_currentState;
    }

    public void SetPlayerRef(GameObject newPlayerRef)
    {
        m_playerRef = newPlayerRef;
    }

    public void MoveToPosition(Vector3 pos1, Vector3 pos2)
    {
        transform.position = Vector3.MoveTowards(pos1, pos2, m_movementSpeed * Time.deltaTime);
    }

    public Vector3 FindNewPatrolPosition()
    {
        Vector3 v3 = Random.insideUnitCircle * 5;
        Vector3 newPos = v3 + transform.position;

        return newPos;
    }

    private Vector3 GetPlayerPosition(GameObject playerRef)
    {
        return playerRef.transform.position;
    }

    private void FaceEnemy(GameObject player)
    {
        Vector3 playerPosition = player.transform.position;
        Vector2 direction = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
        transform.up = direction;
    }
}
