using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using static UnityEngine.Object;

namespace YSFramework
{
    /// <summary>
    /// 拓展工具类，用于实现拓展的工具方法
    /// </summary>
    public static class ExtensionTools
    {
        /// <summary>
        /// 自适应显示UI,根据UI生成位置与屏幕边界来自动调整位置
        /// </summary>
        public static void ShowSelfAdaptingUI(this Image menuImg, Vector2 position)
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float imageWidth = menuImg.GetComponent<RectTransform>().sizeDelta.x;
            float imageHeight = menuImg.GetComponent<RectTransform>().sizeDelta.y;
            if (position.x + imageWidth > (screenWidth))
            {
                menuImg.GetComponent<RectTransform>().pivot = new Vector2(1, menuImg.GetComponent<RectTransform>().pivot.y);
            }
            else
            {
                menuImg.GetComponent<RectTransform>().pivot = new Vector2(0, menuImg.GetComponent<RectTransform>().pivot.y);
            }
            if (position.y - imageWidth < 0)
            {
                menuImg.GetComponent<RectTransform>().pivot = new Vector2(menuImg.GetComponent<RectTransform>().pivot.x, 0);
            }
            else
            {
                menuImg.GetComponent<RectTransform>().pivot = new Vector2(menuImg.GetComponent<RectTransform>().pivot.x, 1);

            }
            menuImg.GetComponent<RectTransform>().position = position;
        }

        /// <summary>
        /// 资源加载方法，通过资源类型和名称来加载对应资源
        /// </summary>
        /// <param name="resourceType">资源类型</param>
        /// <param name="objectName">资源名称</param>
        /// <returns></returns>
        public static object LoadResource(ResourceType resourceType, string objectName)
        {
            object o = null;
            switch (resourceType)
            {
                case ResourceType.Model:
                    o = Resources.Load<GameObject>("Models/" + objectName);
                    break;
                case ResourceType.UIPanel:
                    o = Resources.Load<GameObject>("UIPanels/" + objectName);
                    break;
                case ResourceType.UIItem:
                    o = Resources.Load<GameObject>("UIItems/" + objectName);
                    break;
                case ResourceType.File:
                    o = Resources.Load<TextAsset>("Files/" + objectName);
                    break;
                case ResourceType.Image:
                    o = Resources.Load<Texture2D>("Images/" + objectName);
                    break;
                case ResourceType.Material:
                    o = Resources.Load<Material>("Materials/" + objectName);
                    break;
                case ResourceType.Plant:
                    o = Resources.Load<GameObject>("Plants/" + objectName);
                    break;
                case ResourceType.Projectile:
                    o = Resources.Load<GameObject>("Projectiles/" + objectName);
                    break;
                case ResourceType.Misc:
                    o = Resources.Load<GameObject>("Misc/" + objectName);
                    break;
                default:
                    break;
            }
            return o;
        }
    }
}