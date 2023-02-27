using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == null) return;

        if(collision.gameObject.tag == "Player")
        {
            GameObject playerRef = collision.gameObject;
            UpdateState(state.chasing);
            UpdatePlayerRef(playerRef);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == null) return;

        if(collision.gameObject.tag == "Player")
        {
            UpdateState(state.wandering);
        }
    }

    void UpdateState(state newState)
    {
        GetComponentInParent<INavigable>().SetState(newState);
    }

    void UpdatePlayerRef(GameObject newPlayerRef)
    {
        GetComponentInParent<INavigable>().SetPlayerRef(newPlayerRef);
    }
}
