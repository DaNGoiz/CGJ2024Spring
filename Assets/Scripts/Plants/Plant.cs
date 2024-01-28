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
    [SerializeField]
    /// <summary>
    /// 攻击方向
    /// </summary>
    protected Vector2 attackDirection;

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
    protected float warningTime;
    /// <summary>
    /// X轴朝向
    /// </summary>
    public int dirX;
    /// <summary>
    /// Y轴朝向
    /// </summary>
    public int dirY;
    protected string autoAttackTimerName;
    private void Awake()
    {
        if (attackDirection != Vector2.zero)
            FaceTo(attackDirection);
        else
            SetAttackDirection(Vector2.left);
    }
    protected void SwitchMode(AttackMode mode)
    {
        switch (mode)
        {
            case AttackMode.Auto:
                m_AttackMode = AttackMode.Auto;
                TimerInstance.StartTimer(autoAttackTimerName, reset: true);
                break;
            case AttackMode.Trigger:
                m_AttackMode = AttackMode.Trigger;
                TimerInstance.ResetTimer(autoAttackTimerName);
                break;
        }
    }
    /// <summary>
    /// 改变朝向
    /// </summary>
    /// <param name="dirX">X轴朝向</param>
    /// <param name="dirY">Y轴朝向</param>
    /// <param name="setAnimParam">是否一起改变动画机参数</param>
    public void FaceTo(Vector2 dir, bool setAnimParam = true)
    {
        dirX = dir.x == 0 ? 0 : (int)Mathf.Sign(dir.x);
        dirY = dir.y == 0 ? 0 : (int)Mathf.Sign(dir.y);
        if (setAnimParam)
        {
            m_animator.SetFloat("PosX", dirX);
            m_animator.SetFloat("PosY", dirY);
        }
    }
    public void SetAttackDirection(Vector2 dir)
    {
        attackDirection = dir;
        FaceTo(dir, false);
    }
    protected virtual void Warning(float time) { }
    protected virtual GameObject Attack(GameObject projPrefab, Vector2 dir, float speed, params object[] args)
    {
        return Attack(projPrefab, dir, speed, Vector2.zero, args);
    }
    protected virtual GameObject Attack(GameObject projPrefab, Vector2 dir, float speed, Vector2 offset, params object[] args)
    {
        SetAttackDirection(dir);
        GameObject projObj = ObjPool.RequestObject(projPrefab);
        if (projObj != null)
        {
            if (projObj.TryGetComponent(out Projectile proj))
            {
                proj.Launch((Vector2)transform.position + offset, dir, speed, args);
            }
            else
                Debug.LogError("Object doesn't own \"Projectile\" component");
        }
        else
            Debug.LogError("projectile is null");
        return projObj;
    }
    protected void OnObjectDestroy()
    {
        TimerInstance.RemoveTimer(autoAttackTimerName);
    }
}
