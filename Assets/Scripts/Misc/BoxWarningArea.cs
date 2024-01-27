using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static YSFramework.GlobalManager;

class BoxWarningArea : WarningArea
{
    private enum SpriteType
    {
        BoxStart,
        BoxMiddle,
        BoxEnd,
    }
    public WarningArea CreateWarningArea(Vector2 start, Vector2 dir, int length, float showTime)
    {
        string timerName = TimerInstance.CreateEventTimer("ClearWarningArea", showTime, ClearArea, null, true, false);
        TimerInstance.StartTimer(timerName);
        RaycastHit2D[] hits = Physics2D.RaycastAll(start, dir, length);
        int range = length;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.layer == LayerMask.GetMask(LayerName.Wall))
                range = (int)Mathf.Ceil((hit.point - start).magnitude);
        }
        GameObject spObj;
        GeneratePart(start, SpriteType.BoxStart);
        for (int i = 0; i < (range - 1) * 2; i++)
        {
            GeneratePart(start + dir.normalized / 2 * i, SpriteType.BoxMiddle);
        }
        GeneratePart(start + dir.normalized * (range - 1), SpriteType.BoxEnd);

        void GeneratePart(Vector2 position, SpriteType spriteType)
        {
            spObj = Instantiate(spriteObjectPrefab, transform);
            spObj.GetComponent<SpriteRenderer>().sprite = areaSprites[(int)spriteType];
            spObj.transform.position = position;
            spObj.transform.rotation = Quaternion.identity;
            spObj.transform.Rotate(0, 0, Vector3.SignedAngle(Vector2.right, dir, Vector3.forward));
        }

        return this;
    }
    private void ClearArea(object[] args)
    {
        Destroy(gameObject);
    }
}
