using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;
using static YSFramework.GlobalManager;

public class SpittingMelon : Plant
{
    [SerializeField]
    private Vector2 firePosUp;
    [SerializeField]
    private Vector2 firePosDown;
    [SerializeField]
    private Vector2 firePosLeft;
    [SerializeField]
    private Vector2 firePosRight;
    [SerializeField]
    private GameObject projPrefab;
    private void Start()
    {
        //生成一个随机不重复名字作为计时器名
        do
            timerName = "SpittingMelon" + Random.Range(0f, 100f);
        while (!TimerInstance.CreateCommonTimer(timerName));
        SwitchMode(AttackMode.Auto);
        FaceTo(1, 0);
        projPrefab = (GameObject)ExtensionTools.LoadResource(ResourceType.Projectile, PrefabName.ToxicFumes);
    }
    private void Update()
    {
        if (m_AttackMode == AttackMode.Auto)
            if (TimerInstance.GetTime(timerName) >= atkInterval)
            {
                Attack(projPrefab, new Vector2(-2, -1), new Vector2(-1, 0), 1.5f).transform.parent = transform;
                TimerInstance.ResetTimer(timerName, startImmediately: true);
            }
    }
}