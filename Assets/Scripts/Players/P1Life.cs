using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Life : MonoBehaviour
{
    //SF
    [SerializeField]
    private int life;
    
    //NoSF
    private bool unDead;
    static public int lifeP1;

    public void Damage(int value)
    {
        lifeP1 -= value;
        if(lifeP1<0)
        {

            //dead
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        life = 10;
        unDead = true;
        lifeP1 = life;
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
        lifeP1 = life;
    }

}
