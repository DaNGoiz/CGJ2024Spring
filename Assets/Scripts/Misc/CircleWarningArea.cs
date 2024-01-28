using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YSFramework.GlobalManager;

public class CircleWarningArea : WarningArea
{
    private enum SpriteType
    {
        Circle0,
        Circle1,
        Circle2,
        Circle3
    }
    [SerializeField]
    private Sprite[] spType;
    public WarningArea CreateWarningArea(Vector2 center, float radius, float showTime)
    {
        string timerName = TimerInstance.CreateEventTimer("ClearWarningArea", showTime, ClearArea, null, true, false);
        TimerInstance.StartTimer(timerName);
        transform.position = center;
        int type = Mathf.Min((int)radius, 3);
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.sprite = spType[type];
        return this;
    }
    private void ClearArea(object[] args)
    {
        Destroy(gameObject);
    }
}
