using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YSFramework.GlobalManager;

public class MortarFlower : Plant
{
    [SerializeField]
    private GameObject projPrefab;
    [SerializeField]
    private float blastRadius;
    private int detectCooldown;
    private string atkTimerName;
    private const float detectRange = 2.5f;
    private GameObject target;
    private void Awake()
    {
        if (atkInterval == 0)
            atkInterval = 6f;
    }
    private void Start()
    {
        detectCooldown = 5;
        warningTime = 1.6f;
        atkTimerName = TimerInstance.CreateCommonTimer("MotarFlower");
        TimerInstance.StartTimer(atkTimerName);
    }
    private void Update()
    {
        if (detectCooldown <= 0)
        {
            if (DetectPlayer(detectRange, out target) && TimerInstance.GetTime(atkTimerName) > atkInterval)
            {
                TimerInstance.ResetTimer(atkTimerName);
                StartCoroutine(WarnAndAttack(target.transform.position));
            }
            else
                target = null;
        }
        else
            detectCooldown--;

        IEnumerator WarnAndAttack(Vector3 des)
        {
            WarningArea.CreateCircleArea(target.transform.position, blastRadius, warningTime);
            m_animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.57f);
            Attack(projPrefab, Vector2.zero, 1f, new object[] { des });
            TimerInstance.StartTimer(atkTimerName);
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
