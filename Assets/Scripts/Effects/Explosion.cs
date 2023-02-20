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
        if (collision == null) return;

        GameObject myObject = collision.gameObject;
        if (myObject.GetComponent<Health>() == null) return;

        Health health = myObject.GetComponent<Health>();
        switch (health.CheckResistance())
        {
            case DamageType.lightning:
                health.Heal(damage);
                break;

            default:
                health.Damage(damage);
                break;                
        }
    }
}
