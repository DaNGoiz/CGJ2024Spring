using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static YSFramework.GlobalManager;

public class Plant : MonoBehaviour
{
    [SerializeField]
    protected Animator m_animator;
    [SerializeField]
    protected SpriteRenderer m_renderer;

    /// <summary>
    /// ����ģʽ���Զ���������һ��ʱ�乥��һ�Σ�����ʽ������Ҫ�ⲿ���ù���������Ĭ��Ϊ�Զ�����
    /// </summary>
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
    public int dirX;
    /// <summary>
    /// Y�ᳯ��
    /// </summary>
    public int dirY;
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
    /// <summary>
    /// �ı䳯��
    /// </summary>
    /// <param name="dirX">X�ᳯ��</param>
    /// <param name="dirY">Y�ᳯ��</param>
    /// <param name="setAnimParam">�Ƿ�һ��ı䶯��������</param>
    public void FaceTo(int dirX, int dirY, bool setAnimParam = true)
    {
        this.dirX = dirX;
        this.dirY = dirY;
        if (setAnimParam)
        {
            m_animator.SetFloat("PosX", dirX);
            m_animator.SetFloat("PosY", dirY);
        }
    }
    //��ʱ
    protected void Attack()
    {
        m_animator.SetTrigger("Attack");
    }
    protected void Attack(Projectile proj, Vector2 dir)
    {
        m_animator.SetTrigger("Attack");
    }
    protected void OnObjectDestroy()
    {
        GlobalTimer.DestroyTimer(timerName);
    }
}
