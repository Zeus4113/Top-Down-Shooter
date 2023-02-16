using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float bulletTime;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float force;

    private Collider2D myCollider;
    private Vector3 direction;

    public void Init()
    {
        myCollider = GetComponent<Collider2D>();
        Physics.IgnoreLayerCollision(0, 8);
    }

    public void Update()
    {
        MoveRocket();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        GameObject myObject = collision.gameObject;
        GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);

        if (myObject.GetComponent<Health>() != null)
        {
            Health health = myObject.GetComponent<Health>();
            health.Damage(damage);
            Destroy();
        }

        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rb = myObject.GetComponent<Rigidbody2D>();
            rb.AddRelativeForce(this.transform.up * force, ForceMode2D.Impulse);
            Destroy();
        }
    }

    private void MoveRocket()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        Invoke("Destroy", bulletTime);
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }

    public float GetDamage()
    {
        return damage;
    }

    private void Destroy()
    {
        Debug.Log("Destroy");
        Destroy(this.gameObject);
    }
}
