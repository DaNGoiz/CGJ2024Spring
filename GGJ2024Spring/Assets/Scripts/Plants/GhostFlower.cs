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
    [SerializeField]
    private bool isTrible;
    [SerializeField]
    private float angle;
    protected override void Insane()
    {
        insaneTrigger = true;
    }
    private void Start()
    {
        autoAttackTimerName = TimerInstance.CreateCommonTimer("GhostFlower");
        SwitchMode(AttackMode.Auto);
    }
    private void Update()
    {
        if (canTrackPlayer)
        {
            Vector3 d = TrackPlayer(out bool flag);
            if (flag)
            {
                SetAttackDirection(d);
            }
        }
        if (m_AttackMode == AttackMode.Auto)
            if (TimerInstance.GetTime(autoAttackTimerName) >= atkInterval + initDelay)
            {
                initDelay = 0;
                if (insaneTrigger)
                {
                    atkInterval *= 1f / 2f;
                    insaneTrigger = false;
                }
                StartCoroutine(Atk());
                TimerInstance.ResetTimer(autoAttackTimerName);
            }
        IEnumerator Atk()
        {
            if (isTrible)
            {
                m_animator.SetTrigger("Attack");
                yield return new WaitForSeconds(2f / 3f);
                Vector2 ori = attackDirection;
                Attack(projPrefab, attackDirection, 1.5f, attackDirection.normalized / 2f, null);
                Vector2 r = RotateVector(attackDirection, -angle);
                Attack(projPrefab, r, 1.5f, attackDirection.normalized / 2f, null);
                SetAttackDirection(ori);
                r = RotateVector(ori, angle);
                Attack(projPrefab, r, 1.5f, attackDirection.normalized / 2f, null);
                TimerInstance.StartTimer(autoAttackTimerName);
                SetAttackDirection(ori);
            }
            else
            {
                m_animator.SetTrigger("Attack");
                yield return new WaitForSeconds(2f / 3f);
                Attack(projPrefab, attackDirection, 1.5f, attackDirection.normalized / 2f, null);
                TimerInstance.StartTimer(autoAttackTimerName);
            }

            Vector2 RotateVector(Vector2 vector, float angle)
            {
                float radianAngle = angle * Mathf.Deg2Rad;

                float sinValue = Mathf.Sin(radianAngle);
                float cosValue = Mathf.Cos(radianAngle);

                float rotatedX = vector.x * cosValue - vector.y * sinValue;
                float rotatedY = vector.x * sinValue + vector.y * cosValue;

                return new Vector2(rotatedX, rotatedY);
            }
        }
    }
}
