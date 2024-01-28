using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Shoot : PlayerShoot
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (shootCoolDown >= coolDownLimit)
        {
            shootable = true;
        }
    }
    public override void TrigOrShoot()
    {
        if (Player1CTRL.laughTriggerP1 && shootable)
        {
            Shoot();
            shootable = false;
            shootCoolDown = 0;

        }
        if (!Player1CTRL.laughTriggerP1)
        {
            P1Tolerate.tolerateBarP1 += 10;
        }
    }
}
