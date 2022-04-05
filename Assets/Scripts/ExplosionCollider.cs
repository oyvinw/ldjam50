using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCollider : MonoBehaviour
{
    private ExplosionController controller;

    private void Awake()
    {
        controller = GetComponentInParent<ExplosionController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rigidbody = collision.GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            var projectile = collision.GetComponent<Projectile>();
            if (projectile)
            {
                return;
            }

            var force = ((rigidbody.transform.position - transform.position).normalized) * controller.force;
            rigidbody.AddForce(force);

            var enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.DoDamage(controller.damage);
            }

        }
    }
}
