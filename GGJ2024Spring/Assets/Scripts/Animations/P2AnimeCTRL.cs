using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;

public class P2AnimeCTRL : PlayerAnimationCTRL
{

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < P2Tolerate.phaseP2.Length; i++)//将动画的bool条件赋值
        {
            anim.SetBool($"laughTrig{i}", P2Tolerate.phaseP2[i]);
        }
        //接下来调整动画树xy
        currAnimState = anim.GetCurrentAnimatorStateInfo(0);
        if (Player2CTRL.movingP2)
        {
            anim.SetFloat("direction", Player2CTRL.animDirP2.x, 0.1f, Time.deltaTime);
            anim.SetFloat("moving", Player2CTRL.animDirP2.y, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("direction", Player2CTRL.animDirP2.x);
            anim.SetFloat("moving", Player2CTRL.animDirP2.y);
        }
    }
}
