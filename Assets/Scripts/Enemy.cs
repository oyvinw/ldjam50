using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Color deadColor;
    public int spawnCost = 1;

    public float hp = 1;
    public float movementSpeed = 0.01f;
    public float damage = 1;
    public bool alive = true;
    public float fadeTime;

    private SpriteRenderer sprite;
    private GameController gc;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        gc = FindObjectOfType<GameController>();
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - movementSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (alive)
        {
            var projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                hp -= projectile.damage;

                if (hp <= 0)
                {
                    Die();
                }
                return;
            }
        }
    }

    private void Die()
    {
        alive = false;
        sprite.color = deadColor;
        gc.ReportDead();
    }

    public void Destroy()
    {
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        Color color = sprite.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            color.a = alpha;
            sprite.color = color;
            yield return new WaitForSeconds(.05f);
        }

        Destroy(gameObject);
    }
}
