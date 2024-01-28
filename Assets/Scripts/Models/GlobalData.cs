
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YSFramework
{
    /// <summary>
    /// 用来存储全局静态变量数据
    /// </summary>
    public static class GlobalData
    {
        /// <summary>
        /// 主画布
        /// </summary>
        public static Transform MainCanvas;

        /// <summary>
        /// 编辑器画布
        /// </summary>
        public static Transform EditorCanvas;

        /// <summary>
        /// 播放模式中操作的主摄像机
        /// </summary>
        public static Camera MainCamera = null;

        /// <summary>
        /// 场景中创建的通讯参数字典
        /// </summary>
        public static Dictionary<string, string> CommunicationParaDict = new Dictionary<string, string>();

        /// <summary>
        /// 通讯进程字典
        /// </summary>
        public static Dictionary<string, object> CommunicationProcessDict = new Dictionary<string, object>();
    }
}