using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;
using static YSFramework.GlobalManager;

public class SpittingMelon : Plant
{
    [SerializeField]
    private int attackRange;
    private GameObject projPrefab;
    protected override void Warning(float time)
    {
        WarningArea.CreateBoxArea((Vector2)transform.position + attackDirection.normalized / 2f + Vector2.up / 2f, attackDirection, attackRange, time);
        TimerInstance.StartTimer(TimerInstance.CreateEventTimer("SetAnimArg", time - 2f / 3f, SetAnimArg, null, true, false));
        void SetAnimArg(object[] _)
        {
            m_animator.SetTrigger("Attack");
        }
    }
    private void Start()
    {
        warningTime = 1.5f;
        autoAttackTimerName = TimerInstance.CreateCommonTimer("SpittingMelon");
        SwitchMode(AttackMode.Auto);
        projPrefab = (GameObject)ExtensionTools.LoadResource(ResourceType.Projectile, PrefabName.ToxicFumes);
    }
    private void Update()
    {
        if (m_AttackMode == AttackMode.Auto)
            if (TimerInstance.GetTime(autoAttackTimerName) >= atkInterval)
            {
                StartCoroutine(WarnAndAttack(warningTime));
                TimerInstance.ResetTimer(autoAttackTimerName);
            }
        IEnumerator WarnAndAttack(float time)
        {
            Warning(time);
            yield return new WaitForSeconds(time);
            object[] args = new object[] { 10 };
            Attack(projPrefab, attackDirection, 1.5f, attackDirection.normalized / 2f + Vector2.up / 2f, args).transform.parent = transform;
            TimerInstance.StartTimer(autoAttackTimerName);
        }
    }
}