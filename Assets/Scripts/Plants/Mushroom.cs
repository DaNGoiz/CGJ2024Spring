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
        timerName = TimerInstance.CreateCommonTimer("Mushroom");
        SwitchMode(AttackMode.Trigger);
        FaceTo(Vector2.right, false);
    }
    private void Update()
    {
        if(TimerInstance.GetTime(timerName) >= atkCooldown)
            TimerInstance.ResetTimer(timerName);
    }
    public void TriggerAttack()
    {
        if(TimerInstance.GetTime(timerName) == 0)
        {
            TimerInstance.StartTimer(timerName);
            Attack();
        }    
    }
}