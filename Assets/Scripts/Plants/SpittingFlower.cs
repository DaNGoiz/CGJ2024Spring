using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using YSFramework;
using static YSFramework.GlobalManager;

public class SpittingFlower : Plant
{
    private void Start()
    {
        //生成一个随机不重复名字作为计时器名
        timerName = "SpittingFlower" + Random.Range(0, 100);
        GlobalTimer.CreateTimer(timerName);
        SwitchMode(AttackMode.Auto);
        FaceTo(1, 1);
    }
    private void Update()
    {
        if (m_AttackMode == AttackMode.Auto)
            if (GlobalTimer.GetTime(timerName) >= atkInterval)
            {
                Attack();
                GlobalTimer.ResetTimer(timerName, startImmediately: true);
            }
    }
}