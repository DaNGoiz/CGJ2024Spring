using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Shoot : PlayerShoot
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void TrigOrShoot()
    {
        if (Player2CTRL.laughTriggerP2 && shootable)
        {
            Shoot();
            shootable = false;
            shootCoolDown = 0;
        }
        if (!Player2CTRL.laughTriggerP2)
        {
            P2Tolerate.tolerateBarP2 += 10;
        }
    }
}
