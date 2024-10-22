using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody RB;
    [Range(0,100)]
    public float Speed = 10; // m/s

    void Start()
    {
        RB.velocity = transform.forward * Speed;
    }
    /*
    void OnTriggerEnter(Collider other)
    {  
    }
    */
    private void OnCollisionEnter(Collision collision)
    {
        // detect if i'm hitting a rigidbody
        // and if so, i just add some force to it
        Rigidbody rigidbody = collision.collider.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.AddExplosionForce(100, transform.position, 2.5f);
        }
        /*
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Damage(10f);
        }
        */
        Destroy(gameObject);
    }
}
