using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;
    private Vector3 position;
    private float movementSpeed;

    [SerializeField] private float sprintSpeed;
    [SerializeField] private float walkSpeed;

    public void Init()
    {
        movementSpeed = walkSpeed;
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        position = myRigidbody.position;
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
        position.x = Input.GetAxisRaw("Horizontal") * movementSpeed;
        position.y = Input.GetAxisRaw("Vertical") * movementSpeed;
        myRigidbody.velocity = position;
    }

    private void Sprint()
    {
        if (Input.GetButtonDown("Sprint")) {
            movementSpeed = sprintSpeed;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            movementSpeed = walkSpeed;
        }
    }
}

