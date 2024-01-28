using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Life : MonoBehaviour
{
    //SF
    [SerializeField]
    private int life;

    //NoSF
    private bool unDead;
    static public int lifeP2;//UI要用

    static public void Damage(int value)
    {
        lifeP2 -= value;
        if (lifeP2 < 0)
        {

            //dead
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        life = 10;
        unDead = true;
        lifeP2 = life;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player2CTRL.laughTriggerP2)
        {
            unDead = false;
        }
        if (unDead || lifeP2 > 10)
        {
            lifeP2 = 10;
        }
        life = lifeP2;
    }

}
