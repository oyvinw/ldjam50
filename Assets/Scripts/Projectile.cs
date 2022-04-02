using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force = 40f;
    public float damage = 1f;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    public void FireProjectile(Vector3 angle)
    {
        rb.AddForce(new Vector2(angle.x, angle.y) * force); 
    }

}
