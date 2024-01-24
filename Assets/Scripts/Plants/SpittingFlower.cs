using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using YSFramework;
using static YSFramework.GlobalManager;

public class SpittingFlower : Plant
{
    [SerializeField]
    private Vector2 firePosUp;
    [SerializeField]
    private Vector2 firePosDown;
    [SerializeField]
    private Vector2 firePosLeft;
    [SerializeField]
    private Vector2 firePosRight;
    private void Start()
    {
        //����һ��������ظ�������Ϊ��ʱ����
        do
            timerName = "SpittingFlower" + Random.Range(0f, 100f);
        while (!TimerInstance.CreateTimer(timerName));
        SwitchMode(AttackMode.Auto);
        FaceTo(1, 0);
    }
    private void Update()
    {
        if (m_AttackMode == AttackMode.Auto)
            if (TimerInstance.GetTime(timerName) >= atkInterval)
            {
                Attack();
                TimerInstance.ResetTimer(timerName, startImmediately: true);
            }
    }
}