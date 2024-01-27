using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;
using static YSFramework.GlobalManager;

public class SporeBullet : Projectile
{
    private float countdown;
    public override void Launch(Vector2 start, Vector2 dir, float speed, params object[] args)
    {
        base.Launch(start, dir, speed, args);
        countdown = 15f;
    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.GetMask(LayerName.Player))
        {
            //玩家受击方法
        }
        ObjPool.RecycleObject(gameObject);
    }
    protected override void Update()
    {
        base.Update();
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            ObjPool.RecycleObject(gameObject);
        }
    }
}
