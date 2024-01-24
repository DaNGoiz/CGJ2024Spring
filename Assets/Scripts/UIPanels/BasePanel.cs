using UnityEngine;
using System.Collections;
using DG.Tweening;
using YSFramework;
namespace YSFramework
{
    /// <summary>
    /// UI面板基类，用于将公共字段和方法在此进行实现
    /// </summary>
    public class BasePanel : MonoBehaviour
    {
        /// <summary>
        /// 面板类型
        /// </summary>
        public UIPanelType panelType { get; set; }
        /// <summary>
        /// 界面被显示出来 带数据
        /// </summary>
        public virtual void OnEnter(object data)
        {

            gameObject.SetActive(true);
            if (transform.parent != null)
                transform.SetSiblingIndex(transform.parent.childCount - 1);
            if (GetComponent<CanvasGroup>() != null)
            {
                GetComponent<CanvasGroup>().DOFade(1, 0.1f);
            }

        }

        /// <summary>
        /// 界面暂停
        /// </summary>
        public virtual void OnPause()
        {

        }

        /// <summary>
        /// 界面继续
        /// </summary>
        public virtual void OnResume()
        {

        }

        /// <summary>
        /// 界面不显示,退出这个界面，界面被关系
        /// </summary>
        public virtual void OnExit()
        {
            EventCenter.Broadcast<UIPanelType, UIPanelStatus>(EventCode.PanelStatusChanged, panelType, UIPanelStatus.Close);


            if (GetComponent<CanvasGroup>() != null)
            {
                GetComponent<CanvasGroup>().DOFade(0, 0.1f).OnComplete(() => { gameObject.SetActive(false); });
            }
            else
            {
                gameObject.SetActive(false);
            }

        }


    }
}