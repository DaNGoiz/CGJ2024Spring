using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Tolerate : MonoBehaviour
{
    //SF
    [SerializeField]
    private float tltBarP1;

    //NoSF
    float shakeTime;

    private bool laughing;
    static public float tolerateBarP1;
    
    // Start is called before the first frame update
    void Start()
    {
        shakeTime = 0;
        tolerateBarP1 = 0;
        laughing = false;
    }

    // Update is called once per frame
    void Update()
    {
        shakeTime += Time.deltaTime;
        TolerateLaugh();
        ///
        /// 以下时数值测试用的
        ///
        tltBarP1 = tolerateBarP1;
    }
    void TolerateLaugh()//憋笑条的控制，憋憋
    {
        //当抖动幅度变化时仅一次重置位置
        bool shake30=true, shake50 = true, shake70 = true, shake90 = true;
        if(!laughing)
        {
            //消减条
            if (Player1CTRL.movingP1)
            {
                tolerateBarP1 -= 0.25f * Time.deltaTime;
            }
            else
            {
                tolerateBarP1 -= 2 * Time.deltaTime;
            }
            Vector2 shakePos = new Vector2(0, 0);
            //放在laughing if里，只有一阶段会减少和抖动，二阶段不再减少和抖动
            //50抖动，70剧烈，90超级剧烈
            if (tolerateBarP1 < 0)
            {
                tolerateBarP1 = 0;
            }
            if (tolerateBarP1 < 30)
            {
                shakePos = new Vector2(0, 0);
                transform.localPosition = shakePos;
                shake50 = shake70 = shake90 = true;//恢复正常时使得下一次抖动能重置位置
            }
            else if (tolerateBarP1 < 50)//>30
            {
                if (shake30)
                {
                    transform.localPosition = shakePos = new Vector2(0, 0);
                    shake50 = false;
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
                }
                shakePos.x += 0.3f * Mathf.Sin(shakeTime * 75);
                transform.localPosition = shakePos;
            }
            else if (tolerateBarP1 < 100)//>90
            {
                if (shake90)
                {
                    transform.localPosition = shakePos = new Vector2(0, 0);
                    shake50 = false;
                }
                shakePos.x += 0.5f * Mathf.Sin(shakeTime * 100);
                transform.localPosition = shakePos;
            }
            else if(tolerateBarP1>100)
            {
                laughing = true;
                tolerateBarP1 = 100;
            }
        }
        else
        {
            transform.localPosition = new Vector2(0, 0);//重置回中心
            Player1CTRL.laughTriggerP1 = true;
            Debug.Log("P1进入二阶段");
        }
        if (tolerateBarP1 > 100)
        {
            tolerateBarP1 = 100;
        }

    }
}
