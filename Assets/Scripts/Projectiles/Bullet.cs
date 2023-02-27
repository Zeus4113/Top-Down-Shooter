using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_damage;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_bulletTime;
    [SerializeField] private float m_force;

    private Vector3 m_direction;
    private float m_currentTime;

    public void Init()
    {
        StartCoroutine(MoveObject());
        m_currentTime = 0f;
    }

    private IEnumerator MoveObject()
    {
        do {

            Debug.Log("Moving", gameObject);
            transform.Translate(m_direction * m_speed * Time.deltaTime);
            yield return new WaitForSeconds(1f);
            m_currentTime++;

        } while (m_currentTime < m_bulletTime);

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 2) return;

        if (collision.gameObject.GetComponent<Health>() != null)
        {
            collision.gameObject.GetComponent<Health>().Damage(m_damage);
        }

        Destroy(gameObject);
    }

    public void SetDirection(Vector3 direction)
    {
        this.m_direction = direction.normalized;
    }
}
