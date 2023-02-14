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
        if(collision != null)
        {        
            StartCoroutine(Damage(collision.gameObject));

            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (collision.gameObject.GetComponents<Bruiser>().Length != 0)
                {                   
                    collision.GetComponent<Bruiser>().SetSlowed();
                }
                if (collision.gameObject.GetComponents<Sprinter>().Length != 0)
                {
                    collision.GetComponent<Sprinter>().SetSlowed();
                }
            }
        }
    }

    private IEnumerator Damage(GameObject myObject)
    {
        for(int i = 0; i < duration; i++)
        {
            if(myObject != null)
            {
                if (myObject.gameObject.GetComponent<Health>() != null)
                {
                    if (myObject.gameObject.GetComponents<Shooter>().Length != 0)
                    {
                        myObject.GetComponent<Health>().Heal(damage);
                    }
                    else
                    {
                        myObject.GetComponent<Health>().Damage(damage);
                    }
                }
            }

            yield return new WaitForSeconds(1);
        }

        yield return null;
    }
}