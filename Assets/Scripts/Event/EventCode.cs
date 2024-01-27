namespace YSFramework
{
    /// <summary>
    /// 事件码
    /// </summary>
    public enum EventCode
    {
        /// <summary>
        /// 加载资源
        /// </summary>
        LoadResource,
        /// <summary>
        /// 载入UI面板
        /// </summary>
        PushPanel,
        /// <summary>
        /// 移除顶部面板
        /// </summary>
        PopPanel,
        /// <summary>
        /// 移除特定面板
        /// </summary>
        RemovePanel,
        /// <summary>
        /// 面板状态改变时调用(打开/关闭等)
        /// </summary>
        PanelStatusChanged,
        /// <summary>
        /// 接收通讯值 用于资产接收外部通讯数据
        /// </summary>
        ReadCommunication,
        /// <summary>
        /// 写入通讯值 用户资产写外部通讯数据
        /// </summary>
        WriteCommunication,
        /// <summary>
        /// 发送请求数据 用于系统发送请求
        /// </summary>
        SendRequest,
        /// <summary>
        /// 获得响应数据 用于系统相应请求
        /// </summary>
        GetResponse,
        /// <summary>
        /// 发送映射数据列表到通讯部分
        /// </summary>
        SendDataMappingList,
        /// <summary>
        /// 摄像机聚焦
        /// </summary>
        CameraLookAtTarget,
        /// <summary>
        /// 设置摄像机位置和旋转
        /// </summary>
        SetCameraPosAndRot,
        /// <summary>
        /// 改变text
        /// </summary>
        ChangeText,
        /// <summary>
        /// 改变角色状态状态
        /// </summary>
        SwtichState,
        /// <summary>
        /// 改变砖块状态
        /// </summary>
        SwitchOrganState
    }
}