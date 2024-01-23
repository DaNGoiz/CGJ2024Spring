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
        //生成一个随机不重复名字作为计时器名
        do
            timerName = "Mushroom" + Random.Range(0f, 100f);
        while (!GlobalTimer.CreateTimer(timerName));
        SwitchMode(AttackMode.Trigger);
        FaceTo(1, 0, false);
    }
    private void Update()
    {
        if(GlobalTimer.GetTime(timerName) >= atkCooldown)
            GlobalTimer.ResetTimer(timerName);
    }
    public void TriggerAttack()
    {
        UnityEngine.Debug.Log(GlobalTimer.GetTime(timerName));
        if(GlobalTimer.GetTime(timerName) == 0)
        {
            GlobalTimer.StartTimer(timerName);
            Attack();
        }    
    }
}