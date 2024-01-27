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
    private void Start()
    {
        autoAttackTimerName = TimerInstance.CreateCommonTimer("Mushroom");
        SwitchMode(AttackMode.Trigger);
        FaceTo(Vector2.right, false);
    }
    private void Update()
    {
        if(TimerInstance.GetTime(autoAttackTimerName) >= atkCooldown)
            TimerInstance.ResetTimer(autoAttackTimerName);
    }
    public void TriggerAttack()
    {
        if(TimerInstance.GetTime(autoAttackTimerName) == 0)
        {
            TimerInstance.StartTimer(autoAttackTimerName);
            //Attack();
        }    
    }
}