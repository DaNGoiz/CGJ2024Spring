using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class P1Tolerate : PlayerTolerate
{
    //SF
    [SerializeField]
    private float tltBarP1;

    //NoSF
    float shakeTime;
    static public bool[] phaseP1;
    static public float tolerateBarP1;

    // Start is called before the first frame update
    void Start()
    {
        shakeTime = 0;
        tolerateBarP1 = 0;
        laughing = false;
        phaseP1 = new bool[4] { false, false, false, false };
    }

    // Update is called once per frame
    void Update()
    {
        shakeTime += Time.deltaTime;
        TolerateLaugh();

        tltBarP1 = tolerateBarP1;
    }
    void TolerateLaugh()
    {
        bool shake30 = true, shake50 = true, shake70 = true, shake90 = true;
        if (!laughing)
        {
            if (Player1CTRL.movingP1)
            {
                tolerateBarP1 -= 0.25f * Time.deltaTime;
            }
            else
            {
                tolerateBarP1 -= 2 * Time.deltaTime;
            }
            Vector2 shakePos = new Vector2(0, 0);
            if (tolerateBarP1 < 0)
            {
                tolerateBarP1 = 0;
            }
            if (tolerateBarP1 < 30)
            {
                shakePos = new Vector2(0, 0);
                transform.localPosition = shakePos;
                shake30 = shake50 = shake70 = shake90 = true;
                for (int i = phaseP1.Length - 1; i >= 0; i--)
                {
                    phaseP1[i] = false;
                }
            }
            else if (tolerateBarP1 < 50)//>30
            {
                if (shake30)
                {
                    transform.localPosition = shakePos = new Vector2(0, 0);
                    shake50 = false;
                    phaseP1[0] = true;
                    phaseP1[1] = false;
                }
                shakePos.x += 0.05f * Mathf.Sin(shakeTime * 25);
                transform.localPosition = shakePos;
            }
            else if (tolerateBarP1 < 70)//>50
            {
                if (shake50)
                {
                    transform.localPosition = shakePos = new Vector2(0, 0);
                    shake50 = false;
                    phaseP1[1] = true;
                    phaseP1[2] = false;
                }
                shakePos.x += 0.1f * Mathf.Sin(shakeTime * 50);
                transform.localPosition = shakePos;
            }
            else if (tolerateBarP1 < 90)//>70
            {
                if (shake70)
                {
                    transform.localPosition = shakePos = new Vector2(0, 0);
                    shake70 = false;
                    phaseP1[2] = true;
                    phaseP1[3] = false;
                }
                shakePos.x += 0.1f * Mathf.Sin(shakeTime * 75);
                transform.localPosition = shakePos;
            }
            else if (tolerateBarP1 < 100)//>90
            {
                if (shake90)
                {
                    transform.localPosition = shakePos = new Vector2(0, 0);
                    shake90 = false;
                    phaseP1[3] = true;
                }
                shakePos.x += 0.15f * Mathf.Sin(shakeTime * 100);
                transform.localPosition = shakePos;
            }
            else if (tolerateBarP1 > 100)
            {
                Player1CTRL.laughTriggerP1 = Player2CTRL.laughTriggerP2 = Convert.ToBoolean(SwitchInTrigger(true));
            }
        }
        if (tolerateBarP1 > 100)
        {
            tolerateBarP1 = 100;
        }

        if(Player1CTRL.laughTriggerP1)
        {
            for (int i = 0; i < phaseP1.Length; i++)
            {
                phaseP1[i] = true;
            }
        }
    }
}
