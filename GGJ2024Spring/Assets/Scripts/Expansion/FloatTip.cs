using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace YSFramework
{
    /// <summary>
    /// 浮动提示条，用于在鼠标经过时显示浮动提示信息，离开目标对象后隐藏提示信息
    /// </summary>
    public class FloatTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// 提示内容
        /// </summary>
        public string content;
        /// <summary>
        /// 鼠标指针进入目标对象时调用
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            EventCenter.Broadcast<UIPanelType, object>(EventCode.PushPanel, UIPanelType.Float, content);
        }
        /// <summary>
        /// 鼠标指针离开目标对象时调用
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {

        }
    }
}