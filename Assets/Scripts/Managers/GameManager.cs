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
    /// <param name="phase">0~4���ܹ�5���׶�</param>
    /// <param name="player">1~2,2���</param>
    public void SwitchInTolerant(bool isChangeState,int phase,int player)
    {
        EventCenter.Broadcast<bool>(EventCode.SwtichState, isChangeState);
        switch(phase)
        {
            case 0://0~29
                if(player==1)
                {
                    P1Tolerate.tolerateBarP1 = 0;
                }
                else
                {
                    P2Tolerate.tolerateBarP2 = 0;
                }
                break;
            case 1://30~49
                if (player == 1)
                {
                    P1Tolerate.tolerateBarP1 = 40;
                }
                else
                {
                    P2Tolerate.tolerateBarP2 = 40;
                }
                break;
            case 2://50~69
                if (player == 1)
                {
                    P1Tolerate.tolerateBarP1 = 60;
                }
                else
                {
                    P2Tolerate.tolerateBarP2 = 60;
                }
                break;
            case 3://70~89
                if (player == 1)
                {
                    P1Tolerate.tolerateBarP1 = 80;
                }
                else
                {
                    P2Tolerate.tolerateBarP2 = 80;
                }
                break;
            case 4://90~100
                if (player == 1)
                {
                    P1Tolerate.tolerateBarP1 = 95;
                }
                else
                {
                    P2Tolerate.tolerateBarP2 = 95;
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// ����Ҵ�����ת��Ϊӭս��״̬�л�
    /// </summary>
    /// <param name="isChangeState">trueΪ�ı� falseΪδ�ı�</param>
    /// <param name="player">1~2,2���</param>
    public void SwitchInTrigger(bool isChangeState,int player)
    {
        EventCenter.Broadcast<bool>(EventCode.SwtichState, isChangeState);
        if(player==1)
        {
            P1Tolerate.tolerateBarP1 = 100;
        }
        else
        {
            P2Tolerate.tolerateBarP2 = 100;
        }
    }
}
