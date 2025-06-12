using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageBox", menuName ="DamageBox/Damage")]
public class DamageBox : ScriptableObject
{
    public TrigPoint trigPoint;
    public int value;
    public GameObject Particle;

    public int GetDamageValue() => value;
    public TrigPoint GetType() => trigPoint;
}
public enum TrigPoint
{
    Head,
    Body,
    Leg,
    Arm,
    Shoulder
}
