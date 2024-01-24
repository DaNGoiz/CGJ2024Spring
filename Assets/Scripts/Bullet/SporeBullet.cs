using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;

public class SporeBullet : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.GetMask(LayerName.Player))
        {
            
        }
    }
}
