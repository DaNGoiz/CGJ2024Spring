
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
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

    }
}