using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using YSFramework;
using static YSFramework.GlobalManager;

public class WarningArea : MonoBehaviour
{
    public static WarningArea CreateBoxArea(Vector2 start, Vector2 dir, int length, float showTime)
    {
        GameObject box = (GameObject)ExtensionTools.LoadResource(ResourceType.Misc, PrefabName.BoxWarningArea);
        box = Instantiate(box);
        return box.GetComponent<BoxWarningArea>().CreateWarningArea(start, dir, length, showTime);   
    }    
    public static WarningArea CreateCircleArea(Vector2 center, float radius, float time)
    {
        GameObject circle = (GameObject)ExtensionTools.LoadResource(ResourceType.Misc, PrefabName.CircleWarningArea);
        circle = Instantiate(circle);
        return circle.GetComponent<CircleWarningArea>().CreateWarningArea(center, radius, time);
    }
}
