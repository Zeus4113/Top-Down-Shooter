using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatedFloor : MonoBehaviour, IInteractable
{
    [SerializeField] private float m_damage;
    [SerializeField] private float m_duration;
    [SerializeField] GameObject m_firePrefab;

    private bool m_isActive;

    void Init()
    {

    }

    public void Activate()
    {      
        m_isActive = true;
    }

    public void Deactivate()
    {
        m_isActive = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!m_isActive) return;

        if (collision == null) return;

        GameObject myObject = collision.gameObject;
        if (myObject.GetComponent<Health>() == null) return;

        Health myHealth = myObject.GetComponent<Health>();
        if (myHealth.IsIgnited()) return;

        Ignite(m_damage, m_duration, myObject);
        myHealth.SetIgnited(true);
    }

    public void Ignite(float tickDamage, float duration, GameObject target)
    {
        Health targetHealth = target.GetComponent<Health>();

        if (!targetHealth.IsIgnited())
        {
            // Set is ignited
            targetHealth.SetIgnited(true);

            // Instantiate and attach fire prefab
            GameObject myFire = Instantiate(m_firePrefab, transform.position, transform.rotation);
            myFire.transform.localScale = target.transform.localScale * 1.3f;
            myFire.transform.parent = target.transform;
            myFire.transform.position = target.transform.position;

            StartCoroutine(Ignited(tickDamage, duration, target, myFire));
        }
    }

    public IEnumerator Ignited(float tickDamage, float duration, GameObject target, GameObject myFire)
    {
        Health targetHealth = target.GetComponent<Health>();

        for (int i = 0; i < duration; i++)
        {
            // Check damage resistance of given enemy
            switch (targetHealth.CheckResistance())
            {
                case DamageType.none:

                    targetHealth.Damage(tickDamage);

                    break;

                case DamageType.fire:

                    targetHealth.Heal(tickDamage);

                    break;
            }

            yield return new WaitForSeconds(0.35f);
        }

        targetHealth.SetIgnited(false);
        Destroy(myFire);
        yield return null;

    }

}
