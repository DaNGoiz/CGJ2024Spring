using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using YSFramework;
namespace YSFramework
{
    /// <summary>
    /// UI面板管理类，对所有UI面板的载入、移除进行管理
    /// </summary>
    public class UIManager : Sington<UIManager>
    {
        /// <summary>
        /// 画布对象字段
        /// </summary>
        [SerializeField]
        private Transform canvasTransform;
        /// <summary>
        /// 画布对象属性
        /// </summary>
        private Transform CanvasTransform
        {
            get
            {
                if (canvasTransform == null)
                {
                    canvasTransform = GlobalData.MainCanvas;
                }
                return canvasTransform;
            }
        }
        /// <summary>
        /// 面板字典，根据面板类型和生成的面板对象进行存储，保存所有实例化面板的游戏物体身上的BasePanel组件
        /// </summary>
        public Dictionary<UIPanelType, BasePanel> panelDict = new Dictionary<UIPanelType, BasePanel>();
        /// <summary>
        /// 栈对象，将生成的面板存储到栈对象
        /// </summary>
        private Stack<BasePanel> panelStack;
        /// <summary>
        /// 初始化方法，添加创建面板、移除面板、移除顶层面板的监听事件
        /// </summary>
        protected override void Awake()
        {
            EventCenter.AddListener<UIPanelType, object>(EventCode.PushPanel, PushPanel);
            EventCenter.AddListener(EventCode.PopPanel, PopPanel);
            EventCenter.AddListener<UIPanelType>(EventCode.RemovePanel, RemovePanel);


            base.Awake();
        }

        /// <summary>
        /// 把某个页面入栈，  把某个页面显示在界面上
        /// </summary>
        public void PushPanel(UIPanelType panelType, object initData)
        {
            //BasePanel panel = panelDict.TryGet(panelType);
            //bool isHasPanel=false;
            //if (panel != null)
            //{
            //    isHasPanel = panelStack.Contains(panel);
            //}
            /*panel=*/
            GetPanel(panelType, initData);
            //if (isHasPanel==false&&isUseLayOut)
            //{
            //    GlobalData.DockPanel.RootRegion.Add(null, Enum.GetName(typeof(UIPanelType), panelType), panel.transform, true);
            //}
        }

        /// <summary>
        /// 出栈 ，把页面从界面上移除
        /// </summary>
        public void PopPanel()
        {
            if (panelStack == null)
                panelStack = new Stack<BasePanel>();

            if (panelStack.Count <= 0) return;

            //关闭栈顶页面的显示
            BasePanel topPanel = panelStack.Pop();
            topPanel.OnExit();
            if (panelStack.Count <= 0) return;
            BasePanel topPanel2 = panelStack.Peek();
            topPanel2.OnResume();

        }

        /// <summary>
        /// 把特定页面从界面上移除
        /// </summary>
        public void RemovePanel(UIPanelType panelType)
        {
            BasePanel basePanel = panelDict.TryGet(panelType);
            if (panelStack == null)
                panelStack = new Stack<BasePanel>();
            if (panelStack.Count <= 0) return;
            if (!panelStack.Contains(basePanel)) return;
            List<BasePanel> panelList = new List<BasePanel>();
            panelList.AddRange(panelStack.ToArray());
            panelStack.Clear();
            panelList.Remove(basePanel);
            for (int i = 0; i < panelList.Count; i++)
            {
                panelStack.Push(panelList[panelList.Count - i - 1]);
            }
            basePanel.OnExit();
            //EventCenter.Broadcast<UIPanelType, UIPanelStatus>(EventCode.PanelStatusChanged, panelType, UIPanelStatus.Close);
            if (panelStack.Count <= 0) return;
            BasePanel topPanel2 = panelStack.Peek();
            topPanel2.OnResume();

        }



        /// <summary>
        /// 根据面板类型 得到实例化的面板
        /// </summary>
        /// <returns></returns>
        private BasePanel GetPanel(UIPanelType panelType, object initData)
        {
            if (panelStack == null)
                panelStack = new Stack<BasePanel>();

            //判断一下栈里面是否有页面
            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }
            EventCenter.Broadcast<UIPanelType, UIPanelStatus>(EventCode.PanelStatusChanged, panelType, UIPanelStatus.Open);


            if (panelDict == null)
            {
                panelDict = new Dictionary<UIPanelType, BasePanel>();
            }

            BasePanel panel = panelDict.TryGet(panelType);

            if (panel == null)
            {
                if (panelDict.ContainsKey(panelType))
                {
                    panelDict.Remove(panelType);
                }
                GameObject instPanel = (GameObject)EventCenter.Broadcast_Return<ResourceType, string>(EventCode.LoadResource, ResourceType.UIPanel, Enum.GetName(typeof(UIPanelType), panelType) + "Panel");
                if (instPanel.GetComponent<Canvas>() == null)
                {
                    instPanel.transform.SetParent(CanvasTransform, false);
                }
                panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
                instPanel.GetComponent<BasePanel>().panelType = panelType;
                instPanel.GetComponent<BasePanel>().OnEnter(initData);
                panelStack.Push(instPanel.GetComponent<BasePanel>());
                return instPanel.GetComponent<BasePanel>();
            }
            else
            {
                panel.OnEnter(initData);
                panelStack.Push(panel);
                return panel;
            }

        }

        /// <summary>
        /// 销毁方法，移除 创建面板、移除面板、移除顶层面板的监听事件
        /// </summary>
        protected override void OnDestroy()
        {
            EventCenter.RemoveListener<UIPanelType, object>(EventCode.PushPanel, PushPanel);
            EventCenter.RemoveListener(EventCode.PopPanel, PopPanel);
            EventCenter.RemoveListener<UIPanelType>(EventCode.RemovePanel, RemovePanel);
            base.OnDestroy();
        }

    }


}