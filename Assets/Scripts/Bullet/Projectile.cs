using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Collider2D hitCol;
    /// <summary>
    /// ��ǰ���з���
    /// </summary>
    private Vector2 direction;
    /// <summary>
    /// �����ٶȻ���������ٶȣ�����Ϊ1
    /// </summary>
    private float speed;
    /// <summary>
    /// ���̶�������Ͷ����
    /// </summary>
    /// <param name="start">��ʼ������</param>
    /// <param name="dir">����</param>
    /// <param name="rotation">����</param>
    /// <param name="speed">�䵯�����ٶ�</param>
    public virtual void Launch(Vector2 start, Vector2 dir, float speed, params object[] args)
    {
        transform.position = start;
        direction = dir.normalized;
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