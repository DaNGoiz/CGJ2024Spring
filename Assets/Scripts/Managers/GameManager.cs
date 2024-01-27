using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;

public class GameManager : Sington<GameManager>
{
    /// <summary>
    /// 改变状态时调用该方法
    /// </summary>
    /// <param name="isChangeState">true为改变 false为未改变</param>
    public void SwitchState(bool isChangeState)
    {
        EventCenter.Broadcast<bool>(EventCode.SwtichState, isChangeState);
    }
    /// <summary>
    /// 玩家处于忍受状态时的状态切换
    /// </summary>
    /// <param name="isChangeState">true为改变 false为未改变</param>
    /// <param name="phase">0~4，总共5个阶段</param>
    /// <param name="player">1~2,2玩家</param>
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
    /// 当玩家从忍受转变为迎战的状态切换
    /// </summary>
    /// <param name="isChangeState">true为改变 false为未改变</param>
    /// <param name="player">1~2,2玩家</param>
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
