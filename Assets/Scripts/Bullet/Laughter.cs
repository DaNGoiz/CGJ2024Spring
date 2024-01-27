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
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask(LayerName.Enemies))
        {
            //敌人受击
        }
        else if (collision.gameObject.layer == LayerMask.GetMask(LayerName.Player))
        {
            //玩家受击
        }
    }
}
