using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool isNaturalWall;
    public Door.Direction direction;

    [Header("Components")]
    public GameObject banner;
    public BoxCollider2D collider2d;

    public void ActivateBanner()
    {
        if (!isNaturalWall)
        {
            banner.SetActive(true);
            collider2d.enabled = true;
        }
        else
        {
            collider2d.enabled = true;
        }

    }

    public void DisactivateBanner()
    {
        if (!isNaturalWall)
        {
            banner.SetActive(false);
            collider2d.enabled = false;
        }
    }
}
