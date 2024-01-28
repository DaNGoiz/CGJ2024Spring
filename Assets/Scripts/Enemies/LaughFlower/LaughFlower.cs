using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughFlower : EnemyBase
{
    enemyState state;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider2D>();
        state = enemyState.rest;
    }

    // Update is called once per frame
    void Update()
    {
        StateDecide();
        switch (state)
        {
            case enemyState.rest:
                break;
            case enemyState.active:
                break;
            case enemyState.attack:
                break;
            case enemyState.dead:
                break;
            default:
                break;
        }
    }
    void StateDecide()
    {
        if (!(Player1CTRL.laughTriggerP1 || Player2CTRL.laughTriggerP2))
        {
            state = enemyState.rest;
        }
        else
        {
            state = enemyState.active;
        }
        if (!true)//到达可攻击范围
        {
            state = enemyState.attack;
        }
        if (health <= 0)
        {
            state = enemyState.dead;
        }
    }
}
