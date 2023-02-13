using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private ParticleSystem mySystem;
    private float damage;

    public void Init()
    {
        Debug.Log("INIT");
        mySystem = gameObject.GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GetComponentInParent<Bruiser>().SetPlayerRef(collision.gameObject);
            mySystem.Play();
           
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //collision.gameObject.GetComponent<PlayerController>().Damage(damage);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mySystem.Stop();
        }
    }
}
