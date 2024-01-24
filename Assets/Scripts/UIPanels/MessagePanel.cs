using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace YSFramework
{
    /// <summary>
    /// ��Ϣ��ʾ��壬������ʾ��ʾ��Ϣ�ı�����
    /// </summary>
    public class MessagePanel : BasePanel
    {
       /// <summary>
       /// ��Ϣ�ı�
       /// </summary>
        [SerializeField]
        private Text content;
        /// <summary>
        /// ȷ�ϰ�ť
        /// </summary>
        [SerializeField]
        private Button okBtn;
        /// <summary>
        /// �رհ�ť
        /// </summary>
        [SerializeField]
        private Button closeBtn;
        /// <summary>
        /// �첽��Ϣ���ݣ����ո�ֵ���ı�
        /// </summary>
        private string syncMsg = null;
        
        /// <summary>
        /// ��ʼ��������������Ӷ�UI�ļ����¼�
        /// </summary>
        private void Start()
        {
            okBtn.onClick.AddListener(OnExit);
            closeBtn.onClick.AddListener(OnExit);

        }
        /// <summary>
        /// �������ʱ���÷��������ڽ��ⲿ���ݵ���ʾ��Ϣ��ʾ�������
        /// </summary>
        /// <param name="data"></param>
        public override void OnEnter(object data)
        {
            base.OnEnter(data);
            ShowMessage(data.ToString());
        }

        /// <summary>
        /// ��ʾ��Ϣ��ʾ����
        /// </summary>
        /// <param name="msg"></param>
        private void ShowMessage(string msg)
        {
            syncMsg = msg;
        }
        /// <summary>
        /// ���·����������첽��ʽ����ʾ��Ϣ��ֵ���ı�
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

