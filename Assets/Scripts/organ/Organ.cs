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
    /// ����ש��
    /// </summary>
    Push,
    /// <summary>
    /// ��ʧש��
    /// </summary>
    DisAppear,
    /// <summary>
    /// �ƶ�ש��
    /// </summary>
    Move,
    /// <summary>
    /// ��תש��
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
    OrganType organType;//ש������
    [SerializeField]
    MoveDic MoveDic;//ש���ƶ�����
    [SerializeField]
    float MoveSpeed;//ש���ƶ��ٶ�
    [SerializeField]
    float RotateSpeed;//ש����ת�ٶ��ٶ�
    [SerializeField]
    float MoveVec;//ש���ƶ�����
    

    Tweener tweener;
    Transform NowTrans;//��¼��ǰλ��
    bool isChangeDic;//�Ƿ�ת���ƶ�����
    Vector2 upVec,downVec,LeftVec,RightVec;//�����ƶ���ǰ�������ĸ�����
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
    

    //������������������ƶ����ĸ�λ��
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
