using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float m_duration;
    [SerializeField] private float m_damage;
    [SerializeField] private GameObject m_firePrefab;

    private void Update()
    {
        Destroy(gameObject, m_duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            // collision.gameObject.GetComponent<Health>().Ignite(m_damage, m_duration);

            // Ignite(m_damage, m_duration, collision.gameObject);
        }
    }

    public void Ignite(float tickDamage, float duration, GameObject target)
    {
        Health targetHealth = target.GetComponent<Health>();

        if (!targetHealth.IsIgnited())
        {
            targetHealth.SetIgnited(true);
            GameObject myFire = Instantiate(m_firePrefab, transform.position, transform.rotation);
            myFire.transform.localScale = target.transform.localScale * 1.3f;
            myFire.transform.parent = target.transform;
            myFire.transform.position = target.transform.position;
            StartCoroutine(Ignited(tickDamage, duration, target));
        }
        else
        {
            Debug.Log("Cannot Ignite");
        }
    }

    public IEnumerator Ignited(float tickDamage, float duration, GameObject target)
    {
        Health targetHealth = target.GetComponent<Health>();

        for (int i = 0; i < duration; i++)
        {
            switch (targetHealth.CheckResistance())
            {
                case DamageType.fire:

                    targetHealth.Heal(tickDamage);

                    break;

                default:

                    targetHealth.Damage(tickDamage);

                    break;
            }

            yield return new WaitForSeconds(1f);
        }

        targetHealth.SetIgnited(false);
        yield return null;

    }
}
