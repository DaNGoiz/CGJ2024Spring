using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileUpward : MonoBehaviour
{
    public float height = 5f;
    public float duration = 2f;

    Vector3 startPosition;
    Vector3 endPosition;

    public void MoveInParabola(Vector3 start, Vector3 end)
    {
        startPosition = start;
        endPosition = end;

        transform.position = startPosition;

        Vector3 midPoint = Vector3.Lerp(startPosition, endPosition, 0.5f);
        midPoint.y += height;

        Vector3[] path = new Vector3[] { startPosition, midPoint, endPosition };

        transform.DOPath(path, duration, PathType.CatmullRom).SetEase(Ease.OutQuad).OnComplete(() => { GetComponent<CannonShell>().Boom(); });
    }
}
