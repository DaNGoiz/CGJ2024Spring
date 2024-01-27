using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Tolerate : PlayerTolerate
{
    //SF
    [SerializeField]
    private float tltBarP2;

    //NoSF
    float shakeTime;
    static public bool[] phaseP2;//憋笑阶段,转阶段了吗
    static public float tolerateBarP2;

    // Start is called before the first frame update
    void Start()
    {
        shakeTime = 0;
        tolerateBarP2 = 0;
        laughing = false;
        phaseP2 = new bool[4] { false, false, false, false };
    }

    // Update is called once per frame
    void Update()
    {
        shakeTime += Time.deltaTime;
        TolerateLaugh();

        // 以下时数值测试用的
        tltBarP2 = tolerateBarP2;
    }
    void TolerateLaugh()//憋笑条的控制，憋憋
    {
        //当抖动幅度变化时仅一次重置位置
        bool shake30 = true, shake50 = true, shake70 = true, shake90 = true;
        if (!laughing)
        {
            //消减条
            if (Player2CTRL.movingP2)
            {
                tolerateBarP2 -= 0.25f * Time.deltaTime;
            }
            else
            {
                tolerateBarP2 -= 2 * Time.deltaTime;
            }
            Vector2 shakePos = new Vector2(0, 0);
            //放在laughing if里，只有一阶段会减少和抖动，二阶段不再减少和抖动
            //50抖动，70剧烈，90超级剧烈
            if (tolerateBarP2 < 0)
            {
                tolerateBarP2 = 0;
            }
            if (tolerateBarP2 < 30)
            {
                shakePos = new Vector2(0, 0);
                transform.localPosition = shakePos;
                shake30 = shake50 = shake70 = shake90 = true;//恢复正常时使得下一次抖动能重置位置
                for (int i = phaseP2.Length - 1; i >= 0; i--)
                {
                    phaseP2[i] = false;
                }
            }
            else if (tolerateBarP2 < 50)//>30
            {
                if (shake30)
                {
                    transform.localPosition = shakePos = new Vector2(0, 0);
                    shake30 = false;
                    phaseP2[0] = true;
                    phaseP2[1] = false;//同阶段高状态回到低状态时，写回为低状态
                }
                shakePos.x += 0.05f * Mathf.Sin(shakeTime * 25);
                transform.localPosition = shakePos;

            }
            else if (tolerateBarP2 < 70)//>50
            {
                if (shake50)
                {
                    transform.localPosition = shakePos = new Vector2(0, 0);
                    shake50 = false;
                    phaseP2[1] = true;
                    phaseP2[2] = false;
                }
                shakePos.x += 0.1f * Mathf.Sin(shakeTime * 50);
                transform.localPosition = shakePos;
            }
            else if (tolerateBarP2 < 90)//>70
            {
                if (shake70)
                {
                    transform.localPosition = shakePos = new Vector2(0, 0);
                    shake70 = false;
                    phaseP2[2] = true;
                    phaseP2[3] = false;

                }
                shakePos.x += 0.1f * Mathf.Sin(shakeTime * 75);
                transform.localPosition = shakePos;
            }
            else if (tolerateBarP2 < 100)//>90
            {
                if (shake90)
                {
                    transform.localPosition = shakePos = new Vector2(0, 0);
                    shake50 = false;
                    phaseP2[3] = true;//没有更高的同阶段状态了
                }
                shakePos.x += 0.15f * Mathf.Sin(shakeTime * 100);
                transform.localPosition = shakePos;
            }
            else if (tolerateBarP2 > 100)
            {
                Player1CTRL.laughTriggerP1 = Player2CTRL.laughTriggerP2 = SwitchInTrigger(true);
                Debug.Log("P2进入二阶段");
                
            }
        }
        if (tolerateBarP2 > 100)
        {
            tolerateBarP2 = 100;
        }
        //二阶段处理
        if(Player2CTRL.laughTriggerP2)
        {
            for (int i = 0; i < phaseP2.Length; i++)
            {
                phaseP2[i] = true;
            }
        }
    }
}
