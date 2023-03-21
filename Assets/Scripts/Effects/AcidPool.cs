using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float duration;

    private void Update()
    {
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == null) return;

        if (!collision.gameObject.CompareTag("Enemy")) return;

        StartCoroutine(Damage(collision.gameObject));
        StartCoroutine(SlowedSpeed(collision.gameObject));
    }

    private IEnumerator Damage(GameObject myObject)
    {
        for(int i = 0; i < duration; i++)
        {
            if (myObject == null) break;

            if (myObject.GetComponent<Health>() == null) break;

            Health health = myObject.GetComponent<Health>();

            switch (health.CheckResistance())
            {
				default:
                    health.Damage(damage);
                    break;

                case DamageType.acid:
                    health.Heal(damage);
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    private IEnumerator SlowedSpeed(GameObject myObject)
    {
        if (myObject == null) yield return null;

        if (myObject.GetComponent<INavigable>() == null) yield return null;

        INavigable enemy = myObject.GetComponent<INavigable>();

        enemy.SetSpeedMultiplier(0.3f);

        yield return new WaitForSeconds(3f);

        if (myObject == null) yield return null;

        enemy.SetSpeedMultiplier(1f);

        yield return null;
    }
}
