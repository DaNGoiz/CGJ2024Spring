using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;

public class GameManager : Sington<GameManager>
{
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioSource source;
    /// <summary>
    /// �ı�״̬ʱ���ø÷���
    /// </summary>
    /// <param name="isChangeState">trueΪ�ı� falseΪδ�ı�</param>
    public void SwitchState(bool isChangeState)
    {
        EventCenter.Broadcast(EventCode.SwitchAction);
        EventCenter.Broadcast<bool>(EventCode.SwtichState, isChangeState);
        EventCenter.Broadcast_Return<bool>(EventCode.SwitchInTrigger,isChangeState);
        EventCenter.Broadcast<bool>(EventCode.SwitchOrganState, isChangeState);
        
    }
    
   
}
