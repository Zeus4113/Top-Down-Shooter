using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate;

    private Vector3 currentPosition;
    private Vector3 newPosition;
    private state currentState;
    private GameObject playerRef;
    private Transform firePos;
    private bool canShoot;

    public void Init()
    {
        //gameObject.GetComponentInChildren<Health>().Init();
        canShoot = true;
        firePos = this.transform.Find("FirePos");
        currentState = state.wandering;
        FindNewPosition();
    }

    public void Run()
    {
        gameObject.GetComponentInChildren<Health>().Run();
        currentPosition = transform.position;

        switch (currentState)
        {
            case state.wandering:

                currentSpeed = 4f;
                if (Vector3.Distance(currentPosition, newPosition) > 1.0f)
                {
                    MoveToPosition(currentPosition, newPosition);

                }
                else if (Vector3.Distance(currentPosition, newPosition) <= 1.0f)
                {
                    FindNewPosition();
                }

                break;

            case state.attacking:

                FaceEnemy(playerRef);

                if (canShoot) Shoot();

                break;

            case state.fleeing:

                currentSpeed = 6f;

                MoveToPosition(currentPosition, new Vector3((transform.position.x - playerRef.transform.position.x) * 5, (transform.position.y - playerRef.transform.position.y) * 5, 0f));

                break;

        }

    }

    private void Shoot()
    {
        canShoot = false;
        GameObject newBullet = Instantiate(bullet, firePos.position, Quaternion.identity);
        newBullet.gameObject.GetComponent<Bullet>().Init();
        newBullet.GetComponent<Bullet>().SetDirection(firePos.transform.up);

        Invoke("ResetShot", fireRate);
    }

    private void ResetShot()
    {
        canShoot = true;
    }

    private void FaceEnemy(GameObject player)
    {
        Vector3 playerPosition = player.transform.position;

        Vector2 direction = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
        transform.up = direction;
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