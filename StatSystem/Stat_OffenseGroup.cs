using System;
using UnityEngine;

[Serializable]
public class Stat_OffenseGroup
{
    public Stat attackSpeed;

    // 물리데미지
    public Stat damage;
    public Stat critPower;
    public Stat critChance;
    public Stat armorReduction; // 방어관통?

    // 마법 데미지
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
    public Stat elementalDamage;
}
