using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Life : MonoBehaviour
{
    private int life;
    private bool unDead;
    // Start is called before the first frame update
    void Start()
    {
        life = 10;
        unDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player1CTRL.laughTriggerP1)
        {
            unDead = false;
        }
        if (unDead)
        {
            life = 10;
        }
    }
}
