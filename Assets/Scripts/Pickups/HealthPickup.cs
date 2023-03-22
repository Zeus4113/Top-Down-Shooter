using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

	[SerializeField] private float m_healingAmount;

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject myObject = collision.gameObject;

			Health myHealth = myObject.GetComponent<Health>();

			if(myHealth?.GetHealth() < myHealth.GetMaxHealth())
			{
				myHealth?.Heal(m_healingAmount);
				Destroy(gameObject);
			}
        }
    }
}
