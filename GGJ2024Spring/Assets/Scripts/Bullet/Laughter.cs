using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laughter : Projectile
{
    protected override void Update()
    {
        base.Update();
        if (direction.x < 0)
            transform.localScale = new Vector3(1, -1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}
