using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;

public class GameManager : Sington<GameManager>
{
    /// <summary>
    /// �ı�״̬ʱ���ø÷���
    /// </summary>
    /// <param name="isChangeState">trueΪ�ı� falseΪδ�ı�</param>
    public void SwitchState(bool isChangeState)
    {
        EventCenter.Broadcast<bool>(EventCode.SwtichState, isChangeState);
    }
    /// <summary>
    /// ��Ҵ�������״̬ʱ��״̬�л�
    /// </summary>
    /// <param name="isChangeState">trueΪ�ı� falseΪδ�ı�</param>
    public void SwitchInTolerant(bool isChangeState)
    {
        EventCenter.Broadcast<bool>(EventCode.SwtichState, isChangeState);
    }
    /// <summary>
    /// ����Ҵ�����ת��Ϊӭս��״̬�л�
    /// </summary>
    /// <param name="isChangeState">trueΪ�ı� falseΪδ�ı�</param>
    public void SwitchInTrigger(bool isChangeState)
    {
        EventCenter.Broadcast<bool>(EventCode.SwtichState, isChangeState);
    }
}
