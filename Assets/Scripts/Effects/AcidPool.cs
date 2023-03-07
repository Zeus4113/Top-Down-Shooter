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
        if(collision.gameObject != null)
        {        
            StartCoroutine(Damage(collision.gameObject));
            StartCoroutine(SlowedSpeed(collision.gameObject));
        }
    }

    private IEnumerator Damage(GameObject myObject)
    {
        for(int i = 0; i < duration; i++)
        {
            if (myObject == null) yield return null;

            if (myObject.GetComponent<Health>() == null) yield return null;

            float health = myObject.GetComponent<Health>().GetHealth();

            switch (myObject.GetComponent<Health>().CheckResistance())
            {
                case DamageType.none:
                    myObject.GetComponent<Health>().Damage(damage);
                    break;

                case DamageType.acid:
                    myObject.GetComponent<Health>().Heal(damage);
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
            
        myObject.GetComponent<INavigable>().SetSpeedMultiplier(0.3f);

        yield return new WaitForSeconds(3f);

        myObject.GetComponent<INavigable>().SetSpeedMultiplier(1f);

        yield return null;
    }
}
