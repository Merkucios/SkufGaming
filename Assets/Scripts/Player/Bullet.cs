using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage { get; private set; } = 5;

    private Rigidbody2D rb;
    
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
        
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IDamageble damageable = other.collider.GetComponent<IDamageble>();

        if (damageable is not null)
        {
            damageable.GetDamage(Damage);
            Destroy(gameObject);
        }
    }
}
