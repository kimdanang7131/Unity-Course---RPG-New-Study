using System;
using UnityEngine;

[Serializable]
public class Stat_OffenseGroup
{
    // 물리데미지
    public Stat damage;
    public Stat critPower;
    public Stat critChance;

    // 마법 데미지
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;
}
