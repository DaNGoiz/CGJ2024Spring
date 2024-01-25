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
    /// 攻击模式，自动攻击会间隔一段时间攻击一次，触发式攻击需要外部调用攻击方法。默认为自动攻击
    /// </summary>
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
    public int dirX;
    /// <summary>
    /// Y轴朝向
    /// </summary>
    public int dirY;
    protected string timerName;
    protected void SwitchMode(AttackMode mode)
    {
        switch (mode)
        {
            case AttackMode.Auto:
                m_AttackMode = AttackMode.Auto;
                TimerInstance.StartTimer(timerName, reset: true);
                break;
            case AttackMode.Trigger:
                m_AttackMode = AttackMode.Trigger;
                TimerInstance.ResetTimer(timerName);
                break;
        }
    }
    /// <summary>
    /// 改变朝向
    /// </summary>
    /// <param name="dirX">X轴朝向</param>
    /// <param name="dirY">Y轴朝向</param>
    /// <param name="setAnimParam">是否一起改变动画机参数</param>
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
    //暂时
    protected virtual void Attack()
    {
        m_animator.SetTrigger("Attack");
    }
    /// <returns></returns>
    protected virtual GameObject Attack(GameObject projPrefab, Vector2 dir, Vector2 rotation, float speed)
    {
        GameObject projObj = ObjPool.RequestObject(projPrefab);
        if (projObj != null)
        {
            if (projObj.TryGetComponent(out Projectile proj))
            {
                proj.Launch(transform.position, dir, rotation, speed);
            }
            else
                Debug.LogError("Object doesn't own \"Projectile\" component");
        }
        else
            Debug.LogError("projectile is null");
        m_animator.SetTrigger("Attack");
        return projObj;
    }
    protected void OnObjectDestroy()
    {
        TimerInstance.RemoveTimer(timerName);
    }
}
