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
        //����һ��������ظ�������Ϊ��ʱ����
        do
            timerName = "Mushroom" + Random.Range(0f, 100f);
        while (!TimerInstance.CreateCommonTimer(timerName));
        SwitchMode(AttackMode.Trigger);
        FaceTo(1, 0, false);
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