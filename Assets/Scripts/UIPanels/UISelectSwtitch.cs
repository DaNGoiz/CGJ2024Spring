using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace YSFramework
{
    /// <summary>
    /// UI切换聚焦类，用于通过Tab键切换当前聚焦的UI对象
    /// </summary>
    public class UISelectSwtitch : MonoBehaviour
    {
        /// <summary>
        /// 可切换聚焦的UI列表
        /// </summary>
        [SerializeField]
        private List<Selectable> uiList = new List<Selectable>();
        /// <summary>
        /// UI事件
        /// </summary>
        private EventSystem eventSystem;
        /// <summary>
        /// 当前的聚焦序号
        /// </summary>
        private int index = 0;
        /// <summary>
        /// 开始方法，用于自动获取UI事件并自动获取所有可交互UI
        /// </summary>
        void Start()
        {
            eventSystem = EventSystem.current;
            ////赋予账户输入框焦点
            //eventSystem.SetSelectedGameObject(inputFieldList[index].gameObject, new BaseEventData(eventSystem));
            StartCoroutine(OnInit());
        }
        /// <summary>
        /// 初始化方法，清除旧数据，并自动获取可交互UI
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnInit()
        {
            yield return null;
            uiList.Clear();
            FindChild(transform);
        }
        /// <summary>
        /// 查找可交互子UI
        /// </summary>
        /// <param name="trans"></param>
        void FindChild(Transform trans)
        {
            if (trans.GetComponent<Selectable>() != null)
            {
                uiList.Add(trans.GetComponent<Selectable>());
            }
            //利用for循环 获取物体下的全部子物体
            for (int i = 0; i < trans.childCount; i++)
            {
                FindChild(trans.GetChild(i));
            }
        }


        /// <summary>
        /// 更新方法，控制Tab 按键切换UI焦点的顺序 
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && eventSystem.currentSelectedGameObject != null && uiList.Contains(eventSystem.currentSelectedGameObject.GetComponent<Selectable>()))
            {
                index = uiList.IndexOf(eventSystem.currentSelectedGameObject.GetComponent<Selectable>());
                index++;
                if (index >= uiList.Count) index = 0;
                if (uiList[index] != null)
                    eventSystem.SetSelectedGameObject(uiList[index].gameObject, new BaseEventData(eventSystem));
            }
        }

    }
}