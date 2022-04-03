using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    public Projectile projectile;

    private List<CannonPart> cannonParts;
    private List<SpriteRenderer> cannonSprites;
    private float cannonPartSpacing;
    private int cannonPartNum;

    private void Awake()
    {
        cannonParts = GetComponentsInChildren<CannonPart>().ToList();
        cannonSprites = new List<SpriteRenderer>();
        foreach (var cannonPart in cannonParts)
        {
            cannonSprites.Add(cannonPart.GetComponent<SpriteRenderer>());
        }
    }

    void Update()
    {
        MouseLook();
        if (Input.GetMouseButtonDown(0))
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
        var mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f;
        var cannon_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - cannon_pos.x;
        mouse_pos.y = mouse_pos.y - cannon_pos.y;
        var angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Clamp((angle - 90), -70f, 70f)));
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
                salvo.speedPrProjectile += cannonPart.speed / salvo.numberOfProjectiles;
            }
        }

        return salvo;
    }

    private void Fire(Salvo salvo)
    {
        var firePos = cannonSprites[Mathf.Clamp(cannonPartNum - 1, 0, int.MaxValue)].transform.position;
        var direction = (cannonSprites[Mathf.Clamp(cannonPartNum - 1, 0, int.MaxValue)].transform.position - cannonSprites.First().transform.position).normalized;

        for (int i = 0; i < salvo.numberOfProjectiles; i++)
        {
            direction.x += Random.Range(-salvo.salvoDeviation, salvo.salvoDeviation);
            direction.y += Random.Range(-salvo.salvoDeviation, salvo.salvoDeviation);
            direction.Normalize();

            var firedProjectile = Instantiate(projectile, firePos, transform.rotation);
            firedProjectile.transform.localScale = new Vector3(firedProjectile.transform.localScale.x * salvo.sizeMultiplier, firedProjectile.transform.localScale.y * salvo.sizeMultiplier, firedProjectile.transform.localScale.z);
            firedProjectile.damage = salvo.damagePrProjectile;
            firedProjectile.speed = salvo.speedPrProjectile;

            firedProjectile.FireProjectile(direction);
        }
    }
}
