using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CannonPart : MonoBehaviour
{
    readonly float damagePlusLimit = 1;
    readonly float speedPlusLimit = 15;
    readonly float deviationPlusLimit = 0.01f;
    readonly float fireRateMinusLimit = 0.2f;

    public bool isActive;
    public float damage;
    public float power;
    public float explosive;
    public float deviation;
    public float sizeMultiplier = 1f;
    public float fireDelay;

    private string green = "<color=#aac39e>";
    private string red = "<color=#95392c>";
    private string blue = "<color=#64a1c2>";
    private string orange = "<color=#e76d46>";
    private string endColorAndLine = "</color>\n";

    /// <summary>
    /// 1 is lower boundary because that means no multiplier
    /// </summary>
    [Range(1, float.MaxValue)]
    public float projectileMultiplier;

    /// <summary>
    /// Lower number is rarer
    /// </summary>
    [Range(0.001f, 1f)]
    public float rarity;

    internal string GetDescription()
    {
        var sb = new StringBuilder();
        //Damage
        if (!isActive)
        {
            sb.Append(blue);
            sb.Append("empty");
            sb.Append(endColorAndLine);
            return sb.ToString();
        }

        if (damage > damagePlusLimit)
        {
            sb.Append(green);
            for (float i = damage; i > damagePlusLimit; i =- damagePlusLimit)
            {
                sb.Append("+");
            }
            sb.Append("damage");
            sb.Append(endColorAndLine);
        }

        if (damage < damagePlusLimit)
        {
            sb.Append(red);
            for (float i = damage; i < damagePlusLimit; i =+ damagePlusLimit)
            {
                sb.Append("-");
            }
            sb.Append("damage");
            sb.Append(endColorAndLine);
        }

        if (power > speedPlusLimit)
        {
            sb.Append(green);
            for (float i = power; i > speedPlusLimit; i =- speedPlusLimit)
            {
                sb.Append("+");
            }
            sb.Append("power");
            sb.Append(endColorAndLine);
        }

        if (power < speedPlusLimit)
        {
            sb.Append(red);
            for (float i = power; i < speedPlusLimit; i =+ speedPlusLimit)
            {
                sb.Append("-");
            }
            sb.Append("power");
            sb.Append(endColorAndLine);
        }

        if (projectileMultiplier > 1)
        {
            sb.Append(green);
            for (float i = projectileMultiplier; i > 1; i =- 3)
            {
                sb.Append("+");
            }
            sb.Append("projectiles");
            sb.Append(endColorAndLine);
        }

        if (explosive > 0)
        {
            sb.Append(orange);
            for (float i = projectileMultiplier; i > 1; i =- 3)
            {
                sb.Append("+");
            }
            sb.Append("explosive");
            sb.Append(endColorAndLine);
        }

        if (deviation > deviationPlusLimit)
        {
            sb.Append(red);
            for (float i = deviation; i > deviationPlusLimit; i =- deviationPlusLimit)
            {
                sb.Append("+");
            }
            sb.Append("deviation");
            sb.Append(endColorAndLine);
        }

        if (fireDelay > fireRateMinusLimit)
        {
            sb.Append(red);
            for (float i = fireDelay; i > fireRateMinusLimit; i =- fireRateMinusLimit)
            {
                sb.Append("-");
            }
            sb.Append("fireRate");
            sb.Append(endColorAndLine);
        }

        if (fireDelay < fireRateMinusLimit)
        {
            sb.Append(green);
            for (float i = fireDelay; i < fireRateMinusLimit; i =+ fireRateMinusLimit)
            {
                sb.Append("+");
            }
            sb.Append("damage");
            sb.Append(endColorAndLine);
        }

        return sb.ToString();
    }
}
