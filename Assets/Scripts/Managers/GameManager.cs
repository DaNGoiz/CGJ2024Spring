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
    
}
