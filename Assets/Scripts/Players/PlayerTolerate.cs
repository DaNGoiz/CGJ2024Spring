using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTolerate : MonoBehaviour
{
    public bool laughing;
    public bool laughTrigger;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 当玩家从忍受转变为迎战的状态切换
    /// </summary>
    /// <param name="isChangeState">true为改变 false为未改变</param>
    public bool SwitchInTrigger(bool isChangeState)
    {
        transform.localPosition = new Vector2(0, 0);//重置回中心
        laughing = isChangeState;
        return laughing;
    }
}
