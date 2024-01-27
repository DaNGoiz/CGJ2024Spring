using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using YSFramework;
using static YSFramework.GlobalManager;

public class GhostFlower : Plant
{
    [SerializeField]
    GameObject projPrefab;
    private void Start()
    {
        autoAttackTimerName = TimerInstance.CreateCommonTimer("GhostFlower");
        SwitchMode(AttackMode.Auto);
    }
    private void Update()
    {
        if (m_AttackMode == AttackMode.Auto)
            if (TimerInstance.GetTime(autoAttackTimerName) >= atkInterval)
            {
                StartCoroutine(Atk());
                TimerInstance.ResetTimer(autoAttackTimerName);
            }
        IEnumerator Atk()
        {
            m_animator.SetTrigger("Attack");
            yield return new WaitForSeconds(2f / 3f);
            Attack(projPrefab, attackDirection, 1.5f, attackDirection.normalized / 2f, null);
            TimerInstance.StartTimer(autoAttackTimerName);
        }
    }
}
