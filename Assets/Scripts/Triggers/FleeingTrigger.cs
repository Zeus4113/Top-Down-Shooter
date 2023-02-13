using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingTrigger : MonoBehaviour
{
    private state fleeState = state.fleeing;
    private state attackState = state.attacking;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<Shooter>().SetState(fleeState);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<Shooter>().SetState(attackState);
        }
    }
}
