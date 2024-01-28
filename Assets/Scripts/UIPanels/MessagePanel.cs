using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace YSFramework
{
    /// <summary>
    /// 信息提示面板，用于显示提示信息文本内容
    /// </summary>
    public class MessagePanel : BasePanel
    {
       /// <summary>
       /// 信息文本
       /// </summary>
        [SerializeField]
        private Text content;
        /// <summary>
        /// 确认按钮
        /// </summary>
        [SerializeField]
        private Button okBtn;
        /// <summary>
        /// 关闭按钮
        /// </summary>
        [SerializeField]
        private Button closeBtn;
        /// <summary>
        /// 异步信息数据，最终赋值给文本
        /// </summary>
        private string syncMsg = null;
        
        /// <summary>
        /// 初始化方法，用于添加对UI的监听事件
        /// </summary>
        private void Start()
        {
            okBtn.onClick.AddListener(OnExit);
            closeBtn.onClick.AddListener(OnExit);

        }
        /// <summary>
        /// 载入面板时调用方法，用于将外部传递的提示信息显示在面板上
        /// </summary>
        /// <param name="data"></param>
        public override void OnEnter(object data)
        {
            base.OnEnter(data);
            ShowMessage(data.ToString());
        }

        /// <summary>
        /// 显示信息提示方法
        /// </summary>
        /// <param name="msg"></param>
        private void ShowMessage(string msg)
        {
            syncMsg = msg;
        }
        /// <summary>
        /// 更新方法，用于异步方式将提示信息赋值给文本
        /// </summary>
        private void Update()
        {
            if (!string.IsNullOrEmpty(syncMsg))
            {

                content.text = syncMsg;
                syncMsg = null;
            }
        }
    }
}

