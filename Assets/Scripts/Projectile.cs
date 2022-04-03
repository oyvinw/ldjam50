using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject explosion;
    public Salvo salvo;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    public void FireProjectile(Vector3 angle)
    {
        rb.AddForce(new Vector2(angle.x, angle.y) * salvo.speedPrProjectile); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (salvo.isExplosive)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            var bang = explosion.GetComponent<ExplosionController>();
            bang.ConfigureExplosion(salvo);

            Destroy(gameObject);
        }
    }
}
