using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float damage;
    public float force;

    public void EndExplosion()
    {
        Destroy(gameObject);
    }

    public void ConfigureExplosion(Salvo salvo)
    {
        transform.localScale = new Vector3(salvo.explosionSize, salvo.explosionSize, salvo.explosionSize);
        damage = salvo.explosionDamage;
        force = salvo.explosionForce;
    }
}
