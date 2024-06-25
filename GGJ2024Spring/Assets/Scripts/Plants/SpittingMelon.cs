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
    protected override void Insane()
    {
        insaneTrigger = true;
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
            if (TimerInstance.GetTime(autoAttackTimerName) >= atkInterval + initDelay)
            {
                initDelay = 0;
                if (insaneTrigger)
                {
                    warningTime *= 2f / 3f;
                    atkInterval *= 2f / 3f;
                    insaneTrigger = false;
                }
                StartCoroutine(WarnAndAttack(warningTime));
                TimerInstance.ResetTimer(autoAttackTimerName);
            }
        IEnumerator WarnAndAttack(float time)
        {
            if (canTrackPlayer)
            {
                Vector3 d = TrackPlayer(out bool flag);
                if (flag)
                {
                    SetAttackDirection(d);
                }
            }
            Warning(time);
            yield return new WaitForSeconds(time);
            object[] args = new object[] { 10 };
            Attack(projPrefab, attackDirection, 1.5f, attackDirection.normalized / 2f + Vector2.up / 2f, args).transform.parent = transform;
            TimerInstance.StartTimer(autoAttackTimerName);
        }
    }
}