using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughFlower : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
