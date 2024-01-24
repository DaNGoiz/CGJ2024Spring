using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D hitCol;
    /// <summary>
    /// 当前飞行方向
    /// </summary>
    private Vector2 direction;
    /// <summary>
    /// 飞行速度
    /// </summary>
    private float speed;
    /// <summary>
    /// 朝固定方向发射投射物
    /// </summary>
    /// <param name="start">起始点坐标</param>
    /// <param name="dir">方向</param>
    /// <param name="rotation">朝向</param>
    /// <param name="speed">射弹飞行速度</param>
    public virtual void Launch(Vector2 start, Vector2 dir, Vector2 rotation, float speed)
    {
        transform.position = start;
        direction = dir;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
        this.speed = speed;
    }
    /// <summary>
    /// 朝固定方向发射投射物
    /// </summary>
    /// <param name="start">起始点坐标</param>
    /// <param name="dir">方向</param>
    /// <param name="rotation">朝向</param>
    /// <param name="speed">射弹飞行速度</param>
    public virtual void Launch(Vector2 start, Vector2 dir, Quaternion rotation, float speed)
    {
        transform.position = start;
        direction = dir;
        transform.rotation = rotation;
        this.speed = speed;
    }
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hitCol = GetComponent<Collider2D>();
    }
    protected virtual void Update()
    {
        rb.velocity = direction * speed;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider) { }
}