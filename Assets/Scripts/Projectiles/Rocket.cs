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
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            collision.gameObject.GetComponent<Health>().Damage(damage);
            Destroy();
        }
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(this.transform.up * force, ForceMode2D.Impulse);
            Destroy();
        }

        if (collision.gameObject.tag == "Environment")
        {
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
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
