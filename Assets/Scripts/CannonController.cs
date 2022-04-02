using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    public Projectile projectile;

    private List<CannonPart> cannonParts;

    private void Start()
    {
        cannonParts = GetComponentsInChildren<CannonPart>().ToList();
    }

    void Update()
    {
        MouseLook();
        Fire();
    }

    public void SetCannon(List<(CannonPart, Sprite)> newCannonParts)
    {
        for (int i = 0; i < cannonParts.Count; i++)
        {
            cannonParts[i] = newCannonParts[i].Item1;
            cannonParts[i].GetComponent<SpriteRenderer>().sprite = newCannonParts[i].Item2;
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

    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var firePos = cannonParts.Last().transform.position;
            var direction = (cannonParts.Last().transform.position - cannonParts.First().transform.position).normalized;

            var firedProjectile = Instantiate(projectile, firePos, transform.rotation);
            firedProjectile.FireProjectile(direction);
        }
    }
}
