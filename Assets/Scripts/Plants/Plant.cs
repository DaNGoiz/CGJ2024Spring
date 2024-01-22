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
    /// �Զ�ģʽ�µĹ������
    /// </summary>
    [SerializeField]
    protected float atkInterval;
    /// <summary>
    /// X�ᳯ��
    /// </summary>
    public int DirX;
    /// <summary>
    /// Y�ᳯ��
    /// </summary>
    public int DirY;
    /// <summary>
    /// ����ģʽ���Զ���������һ��ʱ�乥��һ�Σ�����ʽ������Ҫ�ⲿ���ù���������Ĭ��Ϊ�Զ�����
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
