using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : Entity_Stats
{
    private List<string> activeBuff = new List<string>();
    private Inventory_Player inventoy;

    protected override void Awake()
    {
        base.Awake();
        inventoy = GetComponent<Inventory_Player>();
    }

    public bool CanApplyBuffof(string source)
    {
        return activeBuff.Contains(source) == false;
    }

    public void ApplyBuff(BuffEffectData[] buffsToApply, float duration, string source)
    {
        StartCoroutine(BuffCo(buffsToApply, duration, source));
    }

    private IEnumerator BuffCo(BuffEffectData[] buffsToApply, float duration, string source)
    {
        // 버프 이름 저장
        activeBuff.Add(source);

        // 버프 효과 저장
        foreach (var buff in buffsToApply)
        {
            GetStatByType(buff.type).AddModifier(buff.value, source);
        }

        yield return new WaitForSeconds(duration);

        foreach (var buff in buffsToApply)
        {
            GetStatByType(buff.type).RemoveModifier(source);
        }

        inventoy.TriggerUpdateUI();
        activeBuff.Remove(source);
    }
}
