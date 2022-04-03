using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int spawnCost = 1;

    public float hp = 1;
    public float movementSpeed = 0.01f;
    public float damage = 1;
    public bool alive = true;
    public float fadeTime;

    private SpriteRenderer sprite;
    private GameController gc;
    private Vector3 currentAngle;
    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        gc = FindObjectOfType<GameController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentAngle = transform.eulerAngles;
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - movementSpeed);

            currentAngle = new Vector3(
               Mathf.LerpAngle(currentAngle.x, 0, Time.deltaTime),
               Mathf.LerpAngle(currentAngle.y, 0, Time.deltaTime),
               Mathf.LerpAngle(currentAngle.z, 0, Time.deltaTime)
                );

            transform.eulerAngles = currentAngle;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            DoDamage(projectile.salvo.damagePrProjectile);
        }
    }

    public void DoDamage(float damage)
    {
        if (alive)
        {
            hp -= damage;

            if (hp <= 0)
            {
                Die();
            }

            animator.SetTrigger("Damaged");
            audioSource.Play();
        }
    }

    private void Die()
    {
        alive = false;
        gc.ReportDead();
        animator.SetBool("IsAlive", alive);
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
