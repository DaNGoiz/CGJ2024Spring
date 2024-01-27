using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1AnimeCTRL : PlayerAnimationCTRL
{
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < P1Tolerate.phaseP1.Length; i++)
        {
            anim.SetBool($"laughTrig{i}", P1Tolerate.phaseP1[i]);
        }
        currAnimState = anim.GetCurrentAnimatorStateInfo(0);
        if(Player1CTRL.movingP1)
        {
            anim.SetFloat("direction", Player1CTRL.animDirP1.x,0.1f,Time.deltaTime);
            anim.SetFloat("moving", Player1CTRL.animDirP1.y,0.1f,Time.deltaTime);
        }
        else
        {
            anim.SetFloat("direction", Player1CTRL.animDirP1.x);
            anim.SetFloat("moving", Player1CTRL.animDirP1.y);
        }
    }
}
