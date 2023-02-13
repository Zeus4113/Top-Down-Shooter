using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionTrigger : MonoBehaviour
{
    private state chaseState = state.chasing;
    private state wanderState = state.wandering;
    public bool isDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (GetComponentInParent<Sprinter>().GetState() != state.recharging) && (GetComponentInParent<Sprinter>().GetState() != state.supercharged))
        {
            isDetected = true;
            GetComponentInParent<Sprinter>().SetState(chaseState);
            GetComponentInParent<Sprinter>().SetPlayerRef(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (GetComponentInParent<Sprinter>().GetState() != state.recharging) && (GetComponentInParent<Sprinter>().GetState() != state.supercharged))
        {
            isDetected = false;
            GetComponentInParent<Sprinter>().SetState(wanderState);
        }
    }
}
