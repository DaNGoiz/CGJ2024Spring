using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YSFramework
{
    /// <summary>
    /// ����������
    /// </summary>
    public class MainCanvasController : BaseController
    {
        /// <summary>
        /// ��ʼ�����������ڽ���������洢��ȫ�������У����������������
        /// </summary>
        private void Awake()
        {
            GlobalData.MainCanvas = transform;
        }
    }
}