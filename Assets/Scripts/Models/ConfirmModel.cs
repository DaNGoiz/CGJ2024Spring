using System;
namespace YSFramework
{
    /// <summary>
    /// ȷ�����ģ����
    /// </summary>
    public class ConfirmModel
    {
        /// <summary>
        /// �ı���Ϣ
        /// </summary>
        public string data;
        /// <summary>
        /// ȷ�ϻ�ȡ��ʱ���÷�����bool����Ϊture��ʾȷ�ϣ���֮Ϊȡ��
        /// </summary>
        public Action<bool> finishedAction = null;
    }
}