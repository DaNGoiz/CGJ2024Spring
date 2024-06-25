using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationCTRL : MonoBehaviour
{
    public bool moving;
    public Vector2 animDir;
    public Animator anim;
    public AnimatorStateInfo currAnimState;
    public PlayerState state;
    
    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<PlayerState>();
        moving = false;
        animDir = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        AnimationState();
    }
    
    void AnimationState()
    {
        for (int i = 0; i < state.phase.Length; i++)
        {
            anim.SetBool($"laughTrig{i}", state.phase[i]);
        }
        currAnimState = anim.GetCurrentAnimatorStateInfo(0);
        if (moving)
        {
            anim.SetFloat("direction", animDir.x, 0.1f, Time.deltaTime);
            anim.SetFloat("moving", animDir.y, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("direction", animDir.x);
            anim.SetFloat("moving", animDir.y);
        }
        anim.SetBool("isDead",state.isDead);
    }
}
