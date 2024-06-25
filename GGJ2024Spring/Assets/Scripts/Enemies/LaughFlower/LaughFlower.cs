using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughFlower : EnemyBase
{
    enemyState state;
    [SerializeField]
    GameObject bullet;
    Organ organ;
    bool canShoot;
    // Start is called before the first frame update
    protected override void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider2D>();
        state = enemyState.rest;
        findRoad = GetComponent<FindRoad>();
        findRoad.enabled = false;
        organ = GetComponent<Organ>();
        organ.enabled = false;
        findRoad.ChoosePlayer();
        facing = (findRoad.target.transform.position - transform.position);
        health = 3;
        isActive = 1;
        canHit = true;
        canShoot = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.eulerAngles = Vector3.zero;
        facing = (findRoad.target.transform.position - transform.position);
        state = StateDecide();
        switch (state)
        {
            case enemyState.rest://歇
                InRest();
                break;
            case enemyState.active://动
                //自寻路
                InActive();
                break;
            case enemyState.attack://打
                //云底
                InActive();//攻击的时候也要动
                InAttack();
                break;
            case enemyState.dead://寄
                InDead();
                break;
            default:
                break;
        }
    }
    //四个状态
    void InRest()
    {
        enemyCollider.isTrigger = true;
        findRoad.enabled = false;
    }
    void InActive()
    {
        if(isActive==1)
        {
            isActive--;
            StartCoroutine(ActiveWaiting());
            canHit = true;
            canShoot = true;
        }
        findRoad.enabled = true;
        Debug.Log("已启用！");
        enemyCollider.isTrigger = false;
        enemyAnim.SetFloat("directionX", facing.x);
        enemyAnim.SetFloat("directionY", facing.y);
    }
    void InAttack()
    {
        float distance = Math.Abs((findRoad.target.transform.position - transform.position).magnitude);
        if(canShoot&&distance<6)
        {
            EnemyShoot();
            StartCoroutine(SingleShootCoroutine());
        }
        enemyCollider.isTrigger = false;
    }
    void InDead()
    {
        StopCoroutine(SingleShootCoroutine());
        findRoad.enabled = false;
        enemyCollider.isTrigger = true;
    }

    //敌人发射
    void EnemyShoot()
    {
        //方向
        Vector2 direction = findRoad.target.transform.position - transform.position;
        float bulletAngle = Mathf.Atan2(direction.y, direction.x);
        //instantiate
    GameObject enemyBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, bulletAngle * Mathf.Rad2Deg));
    enemyBullet.GetComponent<Projectile>().Launch(transform.position, RotateVector(Vector2.right, bulletAngle * Mathf.Rad2Deg),2f);
    Vector2 RotateVector(Vector2 vector, float angle)
        {
            float radianAngle = angle * Mathf.Deg2Rad;

            float sinValue = Mathf.Sin(radianAngle);
            float cosValue = Mathf.Cos(radianAngle);

            float rotatedX = vector.x * cosValue - vector.y * sinValue;
            float rotatedY = vector.x * sinValue + vector.y * cosValue;

            return new Vector2(rotatedX, rotatedY);
        }
        //Get Rigid
        //AddForce
    }

    /// <summary>
    /// 单发
    /// </summary>
    /// <returns></returns>
    IEnumerator SingleShootCoroutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    IEnumerator ActiveWaiting()
    {
        yield return new WaitForSeconds(2f);
    }
}
