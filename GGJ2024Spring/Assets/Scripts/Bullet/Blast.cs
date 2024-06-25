using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : Projectile
{
    public override void Launch(Vector2 start, Vector2 dir, float speed, params object[] args)
    {
        transform.position = start;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    protected override void Update()
    {
        //Do Nothing
    }
}
