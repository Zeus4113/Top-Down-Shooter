using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float duration;

    private void Update()
    {
        Destroy(gameObject, duration);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.GetComponent<Health>() != null)
            {
                collision.GetComponent<Health>().Damage(damage);

                if (collision.gameObject.GetComponents<Sprinter>().Length != 0)
                {
                    collision.GetComponent<Sprinter>().SetState(state.supercharged);
                }     
            }
        }
    }
}
