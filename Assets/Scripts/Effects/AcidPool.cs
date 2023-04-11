using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float duration;
	[SerializeField] private bool m_isPersistant;

    private void Update()
    {
		if(!m_isPersistant)
		{
			Destroy(gameObject, duration);
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == null) return;

		if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
		{
			StartCoroutine(Damage(collision.gameObject));
			StartCoroutine(SlowedSpeed(collision.gameObject));
		}
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
		if (myObject == null) yield break;

		if (myObject.GetComponent<INavigable>() != null)
		{
			INavigable enemy = myObject.GetComponent<INavigable>();

			enemy.SetSpeedMultiplier(0.3f);

			yield return new WaitForSeconds(3f);

			if (myObject == null) yield break;

			enemy.SetSpeedMultiplier(1f);
		}
		else if (myObject.GetComponent<PlayerController>() != null){

			Debug.Log("Slowing Player");
			PlayerController playerController = myObject.GetComponent<PlayerController>();

			playerController.SetSpeedMultiplier(0.5f);

			yield return new WaitForSeconds(3f);

			if (myObject == null) yield break;

			playerController.SetSpeedMultiplier(1f);
		}

        yield return null;
    }
}
