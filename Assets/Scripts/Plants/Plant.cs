using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YSFramework.GlobalManager;

public class Plant : MonoBehaviour
{
    [SerializeField]
    protected Animator m_animator;
    [SerializeField]
    protected SpriteRenderer m_renderer;

    public enum AttackMode
    {
        Auto,
        Trigger
    }
    protected AttackMode m_AttackMode;

    /// <summary>
    /// 自动模式下的攻击间隔
    /// </summary>
    [SerializeField]
    protected float atkInterval;
    /// <summary>
    /// X轴朝向
    /// </summary>
    public int DirX;
    /// <summary>
    /// Y轴朝向
    /// </summary>
    public int DirY;
    /// <summary>
    /// 攻击模式，自动攻击会间隔一段时间攻击一次，触发式攻击需要外部调用攻击方法。默认为自动攻击
    /// </summary>
    protected string timerName;
    protected void SwitchMode(AttackMode mode)
    {
        switch (mode)
        {
            case AttackMode.Auto:
                m_AttackMode = AttackMode.Auto;
                GlobalTimer.StartTimer(timerName, reset: true);
                break;
            case AttackMode.Trigger:
                m_AttackMode = AttackMode.Trigger;
                GlobalTimer.ResetTimer(timerName);
                break;
        }
    }
    public void FaceTo(int dirX, int dirY)
    {
        m_animator.SetFloat("PosX", dirX);
        DirX = dirX;
        m_animator.SetFloat("PosY", dirY);
        DirY = dirY;
    }
    protected void Attack()
    {
        m_animator.SetTrigger("Attack");
    }
    protected void OnObjectDestroy()
    {
        GlobalTimer.DestroyTimer(timerName);
    }
}
