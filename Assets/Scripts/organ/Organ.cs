using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;
using DG.Tweening;

[System.Serializable]
public enum OrganType
{
    None,
    /// <summary>
    /// 出现砖块
    /// </summary>
    Push,
    /// <summary>
    /// 消失砖块
    /// </summary>
    DisAppear,
    /// <summary>
    /// 移动砖块
    /// </summary>
    Move,
    /// <summary>
    /// 旋转砖块
    /// </summary>
    Rotate,
}

[System.Serializable]
public enum MoveDic
{
    updown,
    leftright
}
public class Organ : MonoBehaviour
{
    [SerializeField]
    OrganType organType;//砖块类型
    [SerializeField]
    MoveDic MoveDic;//砖块移动方向
    [SerializeField]
    float MoveSpeed;//砖块移动速度
    [SerializeField]
    float RotateSpeed;//砖块旋转速度速度
    [SerializeField]
    float MoveVec;//砖块移动距离
    

    Tweener tweener;
    Transform NowTrans;//记录当前位置
    bool isChangeDic;//是否转换移动方向
    Vector2 upVec,downVec,LeftVec,RightVec;//物体移动的前后左右四个方向
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.AddListener<bool>(EventCode.SwitchOrganState, SwitchOrganState);
        NowTrans = this.transform;
        CountVector();
        SwitchOrganState(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (organType)
        {
            case OrganType.None:
                break;
            case OrganType.Rotate:
                this.transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime);
                break;
            default:
                break;
        }

     
    }
    

    //计算出物体上下左右移动的四个位置
    void CountVector()
    {
        upVec.x = NowTrans.position.x + MoveVec;
        LeftVec.y = NowTrans.position.y - MoveVec;
        RightVec.y = NowTrans.position.y + MoveVec;
        downVec.x = NowTrans.position.x - MoveVec;
    }


    void OrganMove()
    {
        tweener.Kill();
        switch (MoveDic)
        {
            case MoveDic.leftright:
                if(isChangeDic)
                {
                    isChangeDic = !isChangeDic;
                    tweener=this.transform.DOMove(upVec, MoveVec / MoveSpeed).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        OrganMove();
                    });
                }else
                {
                    isChangeDic = !isChangeDic;
                    tweener =this.transform.DOMove(downVec, MoveVec / MoveSpeed).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        OrganMove();
                    });
                }
                break;
            case MoveDic.updown:
                if (isChangeDic)
                {
                    isChangeDic = !isChangeDic;
                    tweener =this.transform.DOMove(RightVec, MoveVec / MoveSpeed).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        OrganMove();
                    });
                }
                else
                {
                    isChangeDic = !isChangeDic;
                    tweener =this.transform.DOMove(LeftVec, MoveVec / MoveSpeed).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        OrganMove();
                    });
                }
                break;
            default:
                break;
        }
    }
    void SwitchOrganState(bool ischange)
    {
        if(ischange)
        {
            switch (organType)
            {
                case OrganType.None:
                    break;
                case OrganType.Push:
                    this.gameObject.active = true;
                    break;
                case OrganType.DisAppear:
                    this.gameObject.active = false;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (organType)
            {
                case OrganType.None:
                    break;
                case OrganType.Push:
                    this.gameObject.active = false;
                    break;
                case OrganType.Move:
                    OrganMove();
                    break;
                default:
                    break;
            }
        }
       
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<bool>(EventCode.SwitchOrganState, SwitchOrganState);
    }
}
