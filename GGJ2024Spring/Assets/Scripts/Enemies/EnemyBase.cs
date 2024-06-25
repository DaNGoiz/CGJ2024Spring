using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int isActive;
    public int health;
    public Animator enemyAnim;
    public BoxCollider2D enemyCollider;
    //custom scripts & types
    public FindRoad findRoad;
    public Vector2 facing;
    public enum enemyState
    {
        rest,
        active,
        attack,
        dead
    }
    public bool canHit;
    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    /// <summary>
    /// 伤害判定
    /// </summary>
    public void Damage()
    {
        health -= 1;
    }

    /// <summary>
    /// 决定状态
    /// </summary>

    public enemyState StateDecide()
    {
        enemyState state;
        if (!(PlayerState.laughTrigger))
        {

            state = enemyState.rest;
        }
        else
        {
            Debug.Log("active");
            enemyAnim.SetBool("setActive", true);
            state = enemyState.active;
        }
        if (facing.magnitude < 10&& PlayerState.laughTrigger)//到达可攻击范围
        {
            state = enemyState.attack;
        }
        if (health <= 0)
        {

            enemyAnim.SetBool("isDead", true);
            state = enemyState.dead;
        }
        return state;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==17)
        {
            Damage();
            StartCoroutine(HitCoolDown());
        }
    }
    IEnumerator HitCoolDown()
    {
        canHit = false;
        yield return new WaitForSeconds(1.5f);
        canHit = true;
    }
}
