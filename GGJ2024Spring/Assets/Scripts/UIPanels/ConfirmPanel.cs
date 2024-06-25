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
    /// ȷ����壬һ������ĳ�������ȷ�ϻ�ȡ��
    /// </summary>
    public class ConfirmPanel : BasePanel
    {
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        [SerializeField]
        private Text content;
        /// <summary>
        /// ȷ�ϰ�ť
        /// </summary>
        [SerializeField]
        private Button okBtn;
        /// <summary>
        /// ȡ����ť
        /// </summary>
        [SerializeField]
        private Button cancelBtn;
        /// <summary>
        /// �رհ�ť
        /// </summary>
        [SerializeField]
        private Button closeBtn;
        /// <summary>
        /// ��ɻص�����
        /// </summary>
        private Action<bool> finishAction;
        /// <summary>
        /// ��ʼ��������������Ӷ�UI��ť�ĵ���¼�����
        /// </summary>
        void Awake()
        {
            okBtn.onClick.AddListener(OnOKClick);
            cancelBtn.onClick.AddListener(OnCancelClick);
            closeBtn.onClick.AddListener(OnCancelClick);
        }
        /// <summary>
        /// �������ʱ���÷��������ڽ��ⲿ������ı����ݽ�����ʾ
        /// </summary>
        /// <param name="data"></param>
        public override void OnEnter(object data)
        {
            base.OnEnter(data);
            ConfirmModel model = (ConfirmModel)data;

            ShowConfirm(model.data, model.finishedAction);
        }
        /// <summary>
        /// ȡ����ť����¼�
        /// </summary>
        private void OnCancelClick()
        {
            finishAction(false);
            OnExit();
        }
        /// <summary>
        /// ȷ�ϰ�ť����¼�
        /// </summary>
        private void OnOKClick()
        {
            finishAction(true);
            OnExit();
        }

        /// <summary>
        /// ��ʾ��ʾȷ����Ϣ
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