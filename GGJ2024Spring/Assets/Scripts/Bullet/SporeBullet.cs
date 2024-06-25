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
        transform.rotation = Quaternion.identity;
        transform.Rotate(0, 0, Vector3.SignedAngle(Vector2.right, dir, Vector3.forward));
        countdown = 15f;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
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
