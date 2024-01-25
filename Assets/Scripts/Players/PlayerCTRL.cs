using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;

public class PlayerCTRL : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        EventCenter.AddListener<bool>(EventCode.SwtichState, OnSwitchState);
    }

    public virtual void OnSwitchState(bool arg)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<bool>(EventCode.SwtichState, OnSwitchState);
    }
}
