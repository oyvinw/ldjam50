using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineController : MonoBehaviour
{
    GameController gc;

    private void Start()
    {
        gc = GetComponentInParent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collidingEnemy = collision.GetComponent<Enemy>();
        if (collidingEnemy != null && collidingEnemy.alive)
        {
            gc.DoDamage(collidingEnemy.damage);
            gc.ReportDead();
            collidingEnemy.alive = false;
            collidingEnemy.Destroy();
        }
    }
}
