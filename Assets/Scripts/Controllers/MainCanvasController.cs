using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YSFramework
{
    /// <summary>
    /// 画布控制类
    /// </summary>
    public class MainCanvasController : BaseController
    {
        /// <summary>
        /// 初始化方法，用于将画布对象存储到全局数据中，便于其他代码调用
        /// </summary>
        private void Awake()
        {
            GlobalData.MainCanvas = transform;
        }
    }
}