using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YSFramework;
namespace YSFramework
{
    /// <summary>
    /// 确认面板，一般用于某项操作的确认或取消
    /// </summary>
    public class ConfirmPanel : BasePanel
    {
        /// <summary>
        /// 信息内容
        /// </summary>
        [SerializeField]
        private Text content;
        /// <summary>
        /// 确认按钮
        /// </summary>
        [SerializeField]
        private Button okBtn;
        /// <summary>
        /// 取消按钮
        /// </summary>
        [SerializeField]
        private Button cancelBtn;
        /// <summary>
        /// 关闭按钮
        /// </summary>
        [SerializeField]
        private Button closeBtn;
        /// <summary>
        /// 完成回调方法
        /// </summary>
        private Action<bool> finishAction;
        /// <summary>
        /// 初始化方法，用于添加对UI按钮的点击事件监听
        /// </summary>
        void Awake()
        {
            okBtn.onClick.AddListener(OnOKClick);
            cancelBtn.onClick.AddListener(OnCancelClick);
            closeBtn.onClick.AddListener(OnCancelClick);
        }
        /// <summary>
        /// 进入面板时调用方法，用于将外部传入的文本数据进行显示
        /// </summary>
        /// <param name="data"></param>
        public override void OnEnter(object data)
        {
            base.OnEnter(data);
            ConfirmModel model = (ConfirmModel)data;

            ShowConfirm(model.data, model.finishedAction);
        }
        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void OnCancelClick()
        {
            finishAction(false);
            OnExit();
        }
        /// <summary>
        /// 确认按钮点击事件
        /// </summary>
        private void OnOKClick()
        {
            finishAction(true);
            OnExit();
        }

        /// <summary>
        /// 显示提示确认信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="action"></param>
        private void ShowConfirm(string msg, Action<bool> action)
        {
            content.text = msg;

            finishAction = action;
        }



    }
}