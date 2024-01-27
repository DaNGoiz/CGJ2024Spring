using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Animator enemyAnim;
    public BoxCollider2D enemyCollider;
    public enum enemyState
    {
        rest,
        active,
        attack,
        dead
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
