using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 10.0f;
    private int damage = 25;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Walls")
        {
            Destroy(gameObject);
        }
        else if (col.tag == "Crate")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        else if (col.tag == "Tank")
        {
            col.gameObject.GetComponent<TankController>().Damage(damage);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = -transform.up * speed;
    }

    void Update()
    {
    }
}
