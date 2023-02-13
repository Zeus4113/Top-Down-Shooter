using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruiserDetectionTrigger : MonoBehaviour
{
    private state chaseState = state.chasing;
    private state wanderState = state.wandering;
    public bool isDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDetected = true;
            GetComponentInParent<Bruiser>().SetState(chaseState);
            GetComponentInParent<Bruiser>().SetPlayerRef(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDetected = false;
            GetComponentInParent<Bruiser>().SetState(wanderState);
        }
    }
}
