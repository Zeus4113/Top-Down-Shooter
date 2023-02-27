using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinter : MonoBehaviour, IEffectable
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float rechargeTime;
    [SerializeField] private GameObject explosion;

    private Vector3 currentPosition;
    private Vector3 newPosition;
    private state currentState;
    private GameObject playerRef;

    public void Init()
    {
        gameObject.GetComponentInChildren<Health>().Init();
        currentState = state.wandering;
        currentSpeed = 4.5f;
        FindNewPosition();
    }

    public void Run()
    {
        gameObject.GetComponentInChildren<Health>().Run();
        currentPosition = transform.position;

        switch (currentState)
        {
            case state.wandering:
                if (Vector3.Distance(currentPosition, newPosition) > 1.0f)
                {
                    MoveToPosition(currentPosition, newPosition);

                }
                else if (Vector3.Distance(currentPosition, newPosition) <= 1.0f)
                {
                    FindNewPosition();
                }

                break;

            case state.chasing:

                FaceEnemy(playerRef);
                MoveToPosition(currentPosition, playerRef.transform.position);

                break;

            case state.recharging:

                currentSpeed = 0f;
                Invoke("FinishedRecharge", rechargeTime);

                break;

            case state.supercharged:

                if (playerRef != null)
                {
                    currentSpeed = 14f;
                    FaceEnemy(playerRef);
                    MoveToPosition(currentPosition, playerRef.transform.position);
                    Invoke("Explode", 1.5f);
                }
                else
                {
                    Explode();
                }

                break;
        }
    }

    private void FinishedRecharge()
    {
        if (this.gameObject.GetComponentInChildren<DetectionTrigger>().isDetected == true)
        {
            currentState = state.chasing;
        }
        else if (this.gameObject.GetComponentInChildren<DetectionTrigger>().isDetected == false)
        {
            currentState = state.wandering;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentState == state.chasing)
            {
                Attack();
            }
            else if (currentState == state.supercharged)
            {
                Explode();
            }
        }

    }

    private void Attack()
    {
        Debug.Log("Attacking");
        playerRef.GetComponent<Health>().Damage(attackDamage);
        currentState = state.recharging;
    }

    private void Explode()
    {
        GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void FaceEnemy(GameObject player)
    {
        Vector3 playerPosition = player.transform.position;
        Vector2 direction = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
        transform.up = direction;
    }

    public float GetDamage()
    {
        return attackDamage;
    }

    // IEffectable Functions

    public void SetSlowed()
    {
        StartCoroutine(Slowed());
    }

    public void SetFasted()
    {
        StopCoroutine(Slowed());
    }

    public IEnumerator Slowed()
    {
        float tempSpeed = currentSpeed;
        currentSpeed = tempSpeed / 3;
        yield return new WaitForSeconds(3);

        currentSpeed = tempSpeed;
        yield return null;
    }

    // INavigable Functions

    public void FindNewPosition()
    {
        Vector3 v3 = Random.insideUnitCircle * 5;
        newPosition = v3 + currentPosition;
    }

    public void MoveToPosition(Vector3 pos1, Vector3 pos2)
    {
        transform.position = Vector3.MoveTowards(pos1, pos2, currentSpeed * Time.deltaTime);
    }

    public void SetState(state myState)
    {
        currentState = myState;
    }

    public state GetState()
    {
        return currentState;
    }

    public void SetPlayerRef(GameObject player)
    {
        playerRef = player;
    }
}