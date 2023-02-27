using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float bulletTime;
    [SerializeField] private float force;

    private Collider2D myCollider;
    private Vector3 direction;

    public void Init()
    {
        myCollider = GetComponent<Collider2D>();
        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        Invoke("Destroy", bulletTime);

        while (this.isActiveAndEnabled)
        {
            transform.Translate(direction * speed * Time.deltaTime);     
        }

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            collision.gameObject.GetComponent<Health>().Damage(damage);
        }
        Destroy();
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

}
