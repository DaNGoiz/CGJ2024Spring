using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;

public class PlayerCTRL : MonoBehaviour
{
    [SerializeField]
    public float speed;

    public enum FaceDir//角色移动朝向（非射击朝向）的枚举
    {
        front,
        back,
        left,
        right
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        
    }

    /// <summary>
    /// 碰撞墙壁和已苏醒的敌人时
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {

    }

    /// <summary>
    /// 碰到子弹和未苏醒/寄掉的敌人时
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {

    }


}
