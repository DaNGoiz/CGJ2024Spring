using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using YSFramework;
using static YSFramework.GlobalManager;

public class Mushroom : Plant
{
    [SerializeField]
    private float atkCooldown;
    [SerializeField]
    private bool isTrigger;
    [SerializeField]
    private GameObject blastPrefab;
    [SerializeField]
    private Animator animator;
    private int detectCooldown = 5;
    protected override void Insane()
    {
        insaneTrigger = true;
    }
    private void Start()
    {
        canTrackPlayer = false;
        warningTime = 1.9f;
        autoAttackTimerName = TimerInstance.CreateCommonTimer("Mushroom");
        if (isTrigger)
            SwitchMode(AttackMode.Trigger);
        FaceTo(Vector2.right, false);
    }
    private void Update()
    {
        if (isTrigger && --detectCooldown <= 0 && DetectPlayer(1.5f, out _))
        {
            if (insaneTrigger)
            {
                warningTime *= 1f / 2f;
                atkCooldown *= 1f / 2f;
                insaneTrigger = false;
            }
            TriggerAttack();
        }
        if(TimerInstance.GetTime(autoAttackTimerName) >= atkCooldown)
            TimerInstance.ResetTimer(autoAttackTimerName);
    }
    public void TriggerAttack()
    {
        if(TimerInstance.GetTime(autoAttackTimerName) == 0)
        {
            TimerInstance.StartTimer(autoAttackTimerName);
            StartCoroutine(WarnAndAttack());
        }

        IEnumerator WarnAndAttack()
        {
            WarningArea.CreateCircleArea(transform.position, 1.5f, warningTime);
            yield return new WaitForSeconds(warningTime - 0.4f);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.4f);
            Attack(blastPrefab, Vector2.zero, 1f, new object[] { 1.5f });
        }
    }
    private bool DetectPlayer(float radius, out GameObject player)
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask(LayerName.Player));
        if (col != null)
        {
            player = col.gameObject;
            return true;
        }
        player = null;
        return false;
    }
}