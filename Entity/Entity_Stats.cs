using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;

    public float GetPhysicalDamage(out bool isCrit)
    {
        float baseDamage = offense.damage.GetValue();
        float bonusDamage = major.strength.GetValue();
        float totalBaseDamage = baseDamage + bonusDamage; // 기본 총합데미지 ( 크리 없음 )

        float baseCritChance = offense.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * .3f; // 능력치 보너스 크리티컬 민첩 : + 0.3퍼 민첩
        float critChance = baseCritChance + bonusCritChance; // 보너스 치명타율

        float baseCritPower = offense.critPower.GetValue();
        float bonuseCritPower = major.strength.GetValue() * .5f; // 능력치 보너스 크리티컬 힘 : + 0.3퍼 힘
        float critPower = (baseCritPower + bonuseCritPower) / 100; // 150 / 100 -> 1.5f 곱 / 보너스 치명타데미지

        // 치명타 -> 기본 총합데미지 * 크리티컬 데미지 or 기본총합데미지
        isCrit = Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

        return finalDamage;
    }

    public float GetMaxHealth()
    {
        float baseMaxHealth = maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5;
        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

    public float GetEvasion()
    {
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f; // 보너스 회피 능력치 : +0.5% 민첩

        float totalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 85f;

        float finalEvastion = Mathf.Clamp(totalEvasion, 0, evasionCap); 
        return finalEvastion;
    }
}
