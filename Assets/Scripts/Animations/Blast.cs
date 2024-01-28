using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : Projectile
{
    public override void Launch(Vector2 start, Vector2 dir, float speed, params object[] args)
    {
        transform.position = start;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, (float)args[0], LayerMask.GetMask(LayerName.Player));
        if (cols.Length > 0)
        {
            foreach (Collider2D col in cols)
            {
                //Íæ¼ÒÊÜ»÷
            }
        }
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
