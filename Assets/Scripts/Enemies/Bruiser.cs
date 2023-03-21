using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bruiser : MonoBehaviour
{
    [SerializeField] private float currentSpeed;

    private state currentState;
    private Vector3 currentPosition;
    private Vector3 newPosition;
    private GameObject playerRef;
    private float chargeTime;
    private float attackRange;
    private Coroutine coroutine;

    public void Init()
    {
        Debug.Log("InitBruiser");
        GetComponentInChildren<AttackTrigger>().Init();
        //gameObject.GetComponentInChildren<Health>().Init();
        currentState = state.wandering;
        FindNewPosition();
    }

    public void Run()
    {
        gameObject.GetComponentInChildren<Health>().Run();

        currentPosition = transform.position;
        if (chargeTime > 0)
        {
            chargeTime -= Time.deltaTime;
        }

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

            case state.attacking:

                if (Vector3.Distance(currentPosition, playerRef.transform.position) < 5.0f && Vector3.Distance(currentPosition, playerRef.transform.position) > 2.0f && chargeTime <= 0)
                {
                    Charge();
                }

                break;

            case state.stunned:                

                break;

        }
    }

    private void Charge()
    {
        currentSpeed = currentSpeed * 2;
        Vector3 chargePosition = playerRef.transform.position;
        MoveToPosition(currentPosition, chargePosition);
        chargeTime = 2.5f;
    }

    private void FaceEnemy(GameObject player)
    {
        Vector3 playerPosition = player.transform.position;
        Vector2 direction = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
        transform.up = direction;
    }

    private IEnumerator Slowed()
    {
        float tempSpeed = currentSpeed;
        currentSpeed = tempSpeed / 3;
        yield return new WaitForSeconds(3);

        currentSpeed = tempSpeed;
        yield return null;
    }

    // IEffectable Functions

    public void SetSlowed()
    {
        StartCoroutine(Slowed());
    }

    public void SetFasted()
    {

    }

    public void SetIgnited()
    {
    }

    public void SetDefused()
    {

    }

    // INavigable Functions

    public void FindNewPosition()
    {
        Vector3 v3 = Random.insideUnitCircle * 5;
        newPosition = currentPosition + v3;
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
