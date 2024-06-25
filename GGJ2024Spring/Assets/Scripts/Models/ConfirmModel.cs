using System;
namespace YSFramework
{
    /// <summary>
    /// 确认面板模型类
    /// </summary>
    public class ConfirmModel
    {
        /// <summary>
        /// 文本信息
        /// </summary>
        public string data;
        /// <summary>
        /// 确认或取消时调用方法，bool参数为ture表示确认，反之为取消
        /// </summary>
        public Action<bool> finishedAction = null;
    }
}