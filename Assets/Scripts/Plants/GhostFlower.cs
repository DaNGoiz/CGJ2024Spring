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
    private void Awake()
    {
        attackDirection = Vector3.left;
    }
    private void Start()
    {
        timerName = TimerInstance.CreateCommonTimer("GhostFlower");
        SwitchMode(AttackMode.Auto);
        FaceTo(Vector2.right);
    }
    private void Update()
    {
        if (m_AttackMode == AttackMode.Auto)
            if (TimerInstance.GetTime(timerName) >= atkInterval)
            {
                StartCoroutine(Atk());
                TimerInstance.ResetTimer(timerName);
            }
        IEnumerator Atk()
        {
            m_animator.SetTrigger("Attack");
            yield return new WaitForSeconds(2f / 3f);
            Attack(projPrefab, attackDirection, 1.5f, attackDirection.normalized / 2f, null);
            TimerInstance.StartTimer(timerName);
        }
    }
}
