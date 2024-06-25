using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YSFramework;
namespace YSFramework
{
    /// <summary>
    /// 场景载入面板，在加载场景时可显示此面板
    /// </summary>
    public class LoadingPanel : BasePanel
    {
        /// <summary>
        /// 信息文本
        /// </summary>
        [SerializeField]
        private Text dataText;
        /// <summary>
        /// 载入面板时调用方法，用于动态显示文本信息
        /// </summary>
        /// <param name="data"></param>

        public override void OnEnter(object data)
        {
            base.OnEnter(data);
            OnShowLoadingData(data.ToString());
        }
        /// <summary>
        /// 显示载入信息方法
        /// </summary>
        /// <param name="data"></param>
        private void OnShowLoadingData(string data)
        {
            dataText.text = data;
        }


    }
}