using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrigger : MonoBehaviour
{
    private state attackState = state.attacking;
    private state wanderState = state.wandering;
    public bool isDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDetected = true;
            GetComponentInParent<Shooter>().SetState(attackState);
            GetComponentInParent<Shooter>().SetPlayerRef(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDetected = false;
            GetComponentInParent<Shooter>().SetState(wanderState);
        }
    }
}
