using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPart : MonoBehaviour
{
    public bool isActive;
    public float damage;
    public float speed;
    public bool fireProjectile;
    public float deviation;
    public float sizeMultiplier = 1f;

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
}
