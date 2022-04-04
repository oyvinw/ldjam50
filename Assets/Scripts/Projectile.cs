using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private float velocityLowerBounds = 1f;
    public Salvo salvo;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    public void FireProjectile(Vector3 angle)
    {
        rb.AddForce(new Vector2(angle.x, angle.y) * salvo.speedPrProjectile); 
        transform.localScale = new Vector3(transform.localScale.x * salvo.sizeMultiplier, transform.localScale.y * salvo.sizeMultiplier, transform.localScale.z);
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
        else
        {
            StartCoroutine(DestroyAfterTime(3f));
        }
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        var t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
