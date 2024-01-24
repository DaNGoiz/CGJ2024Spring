using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2CTRL : MonoBehaviour
{
    //with SF
    #region SF
    [SerializeField]
    private float speed;

    #endregion

    //without SF
    #region NoSF

    private bool isLocked;
    private SpriteRenderer playerSprite;
    private Transform shootPoint;
    private Transform facing;
    static public bool movingP2;
    static public bool laughTriggerP2;
    #endregion
    /// <summary>
    /// 状态转换：是否移动
    /// </summary>
    /// <param name="_moving">true为正在移动，false为静止</param>
    static public void IsMovingP2(bool _moving)
    {
        if (_moving)
        {

        }
        else
        {

        }
    }
    /// <summary>
    /// 状态转换：是否进入大笑(2)阶段
    /// </summary>
    /// <param name="_laughTrig">true为进入二段，false为还没有</param>
    static void IsLaughP2(bool _laughTrig)
    {
        if (_laughTrig)
        {

        }
        else
        {

        }
    }

    /// <summary>
    /// 状态移动
    /// </summary>
    /// <param name="_moving">true为正在移动，false为静止</param>
    static public void MovingP2(bool _moving)
    {
        if(_moving)
        {

        }
        else
        {

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(1, 0);
        playerSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();//直接把角色拉到子物体里面，这样方便控制憋不住笑的震动
        isLocked = false;
        facing = transform.GetChild(0);
        facing.localPosition = new Vector2(0, 0);
        shootPoint = transform.GetChild(2);
        shootPoint.localPosition = new Vector2(2, 0);
        //static
        movingP2 = false;
        laughTriggerP2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        //啊啊啊啊啊啊啊啊啊我不管你朝向标志必须在最上面！！！
        facing.localPosition = new Vector3(facing.localPosition.x, facing.localPosition.y, -0.01f);
        shootPoint.localPosition = new Vector3(shootPoint.localPosition.x, shootPoint.localPosition.y, -0.05f);
        Vector2 pos = transform.position;
        //udlr to move
        movingP2 = false;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movingP2 = true;
            if (!isLocked)
            {
                Vector2 facePos = facing.position;
                facePos.y += speed * 10 * Time.deltaTime;
                facing.position = facePos;
            }
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movingP2 = true;
            if (!isLocked)
            {
                Vector2 facePos = facing.position;
                facePos.y -= speed * 10 * Time.deltaTime;
                facing.position = facePos;
            }
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movingP2 = true;
            if (!isLocked)
            {
                Vector2 facePos = facing.position;
                facePos.x -= speed * 10 * Time.deltaTime;
                facing.position = facePos;
                if (facing.localPosition.x < 0)
                {
                    playerSprite.flipX = true;
                }
            }
            pos.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movingP2 = true;
            if (!isLocked)
            {
                Vector2 facePos = facing.position;
                facePos.x += speed * 10 * Time.deltaTime;
                facing.position = facePos;
                if (facing.localPosition.x > 0)
                {
                    playerSprite.flipX = false;
                }
            }
            pos.x += speed * Time.deltaTime;
        }

        //手感优化，保证锁定方向的手感优良
        if (!isLocked)//只有没锁定时虚拟轴才会动
        {
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                facing.localPosition = new Vector2(facing.localPosition.x, 0);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                facing.localPosition = new Vector2(0, facing.localPosition.y);
            }
        }
        //类似虚拟轴的朝向变化方式
        if (Math.Abs(facing.localPosition.x) > 2)
        {
            facing.localPosition = new Vector2(Math.Sign(facing.localPosition.x) * 2, facing.localPosition.y);
        }
        if (Math.Abs(facing.localPosition.y) > 2)
        {
            facing.localPosition = new Vector2(facing.localPosition.x, Math.Sign(facing.localPosition.y) * 2);
        }
        //锁定后朝向轴调至最远
        if (isLocked)
        {
            if (Math.Abs(facing.localPosition.x) < 2)
            {
                facing.localPosition = new Vector2(Math.Sign(facing.localPosition.x) * 2, facing.localPosition.y);
            }
            if (Math.Abs(facing.localPosition.y) < 2)
            {
                facing.localPosition = new Vector2(facing.localPosition.x, Math.Sign(facing.localPosition.y) * 2);
            }
        }
        transform.position = pos;
        //move完了

        //笑，发射笑子弹和锁定方向
        //Lock <,

        if (Input.GetKey(KeyCode.Comma))
        {
            isLocked = true;
        }
        if (Input.GetKeyUp(KeyCode.Comma))
        {
            isLocked = false;
        }
        //射击方向调整
        //不同时为0时才能让射击点移动
        if ((facing.localPosition.x == 0 && facing.localPosition.y != 0) || (facing.localPosition.x != 0 && facing.localPosition.y == 0) || (facing.localPosition.x != 0 && facing.localPosition.y != 0))
        {
            //x
            if (facing.localPosition.x == 0)
            {
                shootPoint.localPosition = new Vector2(0, shootPoint.localPosition.y);
            }
            else
            {
                shootPoint.localPosition = new Vector2(Math.Sign(facing.localPosition.x) * 2, shootPoint.localPosition.y);
            }
            //y
            if (facing.localPosition.y == 0)
            {
                shootPoint.localPosition = new Vector2(shootPoint.localPosition.x, 0);
            }
            else
            {
                shootPoint.localPosition = new Vector2(shootPoint.localPosition.x, Math.Sign(facing.localPosition.y) * 2);
            }
        }
    }
}