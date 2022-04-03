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

    private void OnTriggerExit2D(Collider2D collision)
    {
        var collidingEnemy = collision.GetComponent<Enemy>();
        if (collidingEnemy != null)
        {
            gc.DoDamage(collidingEnemy.damage);
            gc.ReportDead();
            collidingEnemy.Destroy();
        }
    }
}
