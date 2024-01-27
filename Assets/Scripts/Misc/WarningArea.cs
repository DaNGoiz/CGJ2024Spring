using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;
using static YSFramework.GlobalManager;

public class WarningArea : MonoBehaviour
{
    [SerializeField]
    protected Sprite[] areaSprites;
    [SerializeField]
    protected GameObject spriteObjectPrefab;
    public static WarningArea CreateBoxArea(Vector2 start, Vector2 dir, int length, float showTime)
    {
        GameObject box = (GameObject)ExtensionTools.LoadResource(ResourceType.Misc, PrefabName.BoxWarningArea);
        box = Instantiate(box);
        return box.GetComponent<BoxWarningArea>().CreateWarningArea(start, dir, length, showTime);   
    }    
}
