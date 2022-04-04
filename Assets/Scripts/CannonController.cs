using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    public Projectile projectile;

    private CannonPart[] cannonParts;
    private SpriteRenderer[] cannonSprites;
    private float cannonPartSpacing;
    private int cannonPartNum;
    private Animator anim;
    private AudioSource audioSource;
    private bool canFire = true;
    private bool canMove = true;

    private void Awake()
    {
        cannonParts = new CannonPart[5];
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        cannonSprites = GetComponentsInChildren<SpriteRenderer>();
    }

    public void DisableCannon()
    {
        canFire = false;
        canMove = false;
        transform.eulerAngles = new Vector3(0,0,-70);
    }
    public void EnableCannon()
    {
        canFire = true;
        canMove = true;
    }

    void Update()
    {
        MouseLook();
        if (Input.GetMouseButton(0))
        {
            Fire(CalculateProjectiles());
        }
    }

    public void SetCannon(List<(CannonPart, Sprite, Color)> newCannonParts)
    {
        cannonPartNum = 0;
        for (int i = 0; i < newCannonParts.Count; i++)
        {
            cannonParts[i] = newCannonParts[i].Item1;
            cannonSprites[i].sprite = newCannonParts[i].Item2;
            cannonSprites[i].color = newCannonParts[i].Item3;

            if (cannonSprites[i].sprite != null)
            {
                cannonPartNum++;
                cannonParts[i].isActive = true;
            }
        }
    }

    private void MouseLook()
    {
        if (canMove)
        {
            var mouse_pos = Input.mousePosition;
            mouse_pos.z = 5.23f;
            var cannon_pos = Camera.main.WorldToScreenPoint(transform.position);
            mouse_pos.x = mouse_pos.x - cannon_pos.x;
            mouse_pos.y = mouse_pos.y - cannon_pos.y;
            var angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Clamp((angle - 90), -70f, 70f)));
        }
    }

    private Salvo CalculateProjectiles()
    {
        Salvo salvo = new Salvo();

        foreach (var cannonPart in cannonParts)
        {
            if (cannonPart.isActive)
            {
                salvo.numberOfProjectiles *= cannonPart.projectileMultiplier;
                salvo.sizeMultiplier *= cannonPart.sizeMultiplier;
                salvo.salvoDeviation += cannonPart.deviation;

                salvo.damagePrProjectile += cannonPart.damage / salvo.numberOfProjectiles;
                salvo.speedPrProjectile += cannonPart.power / salvo.numberOfProjectiles;
                salvo.cooldown += cannonPart.fireDelay;

                if (!salvo.isExplosive)
                {
                    salvo.isExplosive = (cannonPart.explosive > 0);
                }
                if (salvo.isExplosive)
                {
                    salvo.explosionDamage += ((salvo.damagePrProjectile / 2) * cannonPart.explosive);
                    salvo.explosionForce += ((salvo.speedPrProjectile / 2) * cannonPart.explosive);
                    salvo.explosionSize += ((salvo.sizeMultiplier / 2) * cannonPart.explosive);
                }
            }
        }

        return salvo;
    }

    private void Fire(Salvo salvo)
    {
        if (canFire)
        {
            var firePos = cannonSprites[Mathf.Clamp(cannonPartNum - 1, 0, int.MaxValue)].transform.position;
            var direction = (cannonSprites[Mathf.Clamp(cannonPartNum - 1, 0, int.MaxValue)].transform.position - cannonSprites.First().transform.position).normalized;

            for (int i = 0; i < salvo.numberOfProjectiles; i++)
            {
                direction.x += Random.Range(-salvo.salvoDeviation, salvo.salvoDeviation);
                direction.y += Random.Range(-salvo.salvoDeviation, salvo.salvoDeviation);
                direction.Normalize();

                var firedProjectile = Instantiate(projectile, firePos, transform.rotation);
                firedProjectile.salvo = salvo;

                firedProjectile.FireProjectile(direction);
            }

            anim.SetTrigger("Fire");
            audioSource.Play();
            StartCoroutine(Cooldown(salvo));
        }
    }

    private IEnumerator Cooldown(Salvo salvo)
    {
        canFire = false;
        var t = 0f;
        while (t < salvo.cooldown)
        {
            t += Time.deltaTime;
            yield return null;
        }

        canFire = true;
    }
}
