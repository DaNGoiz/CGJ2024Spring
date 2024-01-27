using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YSFramework.GlobalManager;

public class MortarFlower : Plant
{
    [SerializeField]
    private GameObject projPrefab;
    private int detectCooldown;
    private string atkTimerName;
    private const float detectRange = 2.5f;
    private const float atkCooldown = 6f;
    private GameObject target;
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
    private void Start()
    {
        detectCooldown = 5;
        atkTimerName = TimerInstance.CreateCommonTimer("MotarFlower");
        TimerInstance.StartTimer(atkTimerName);
    }
    private void Update()
    {
        if (detectCooldown <= 0)
        {
            if (DetectPlayer(detectRange, out target) && TimerInstance.GetTime(atkTimerName) > atkCooldown)
            {
                TimerInstance.ResetTimer(atkTimerName);
                StartCoroutine(WarnAndAttack());
            }
            else
            {
                target = null;
            }
        }
        else
            detectCooldown--;

        IEnumerator WarnAndAttack()
        {
            Warning();
            m_animator.SetTrigger("Attack");
            yield return new WaitForSeconds(2f / 3f);
            Attack();
            TimerInstance.StartTimer(autoAttackTimerName);
        }
    }
    private void Warning()
    {

    }
    private void Attack()
    {

    }
}
