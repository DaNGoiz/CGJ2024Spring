using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// PlayerState包含了玩家的状态调整与生命值,以及动画控制
/// </summary>
public class PlayerState : MonoBehaviour
{
    #region variables

    #region LifeCTRL
    [SerializeField]
    public int life;

    //NoSF
    private bool unDead;
    #endregion
    #region State
    float shakeTime;
    public bool[] phase;//憋笑阶段,转阶段了吗

    [SerializeField]
    public float tolerateBar;
    public Transform playerSpr;
    public bool isDead;
    static public bool laughTrigger;
    static int laughCount;

    #endregion

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        shakeTime += Time.deltaTime;
        Tolerate();
        LifePhase();
    }

    #region Custom Func
    void Init()
    {
        //-------Life
        life = 10;
        unDead = true;
        //-------Tolerate
        shakeTime = 0;
        tolerateBar = 0;
        phase = new bool[4] { false, false, false, false };
        laughTrigger = false;
        laughCount = 1;
    }

    #region LifeCTRL

    public void Damage(int value)
    {
        life -= value;
        if (life <= 0)
        {
            isDead = true;
            life = 0;
            //dead
            Dead();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 18)
        {
            Damage(1);
        }
    }
    #endregion
    #region State
    public void Tolerate()
    {
        //当抖动幅度变化时仅一次重置位置
        bool shake30 = true, shake50 = true, shake70 = true, shake90 = true;
        if (!laughTrigger)
        {
            //消减条
            if (Player1CTRL.movingP1)
            {
                tolerateBar -= 0.25f * Time.deltaTime;
            }
            else
            {
                tolerateBar -= 2 * Time.deltaTime;
            }
            Vector2 shakePos = new Vector2(0, 0);
            //放在laughing if里，只有一阶段会减少和抖动，二阶段不再减少和抖动
            //50抖动，70剧烈，90超级剧烈
            if (tolerateBar < 0)
            {
                tolerateBar = 0;
            }
            if (tolerateBar < 30)
            {
                shakePos = new Vector2(0, 0);
                playerSpr.localPosition = shakePos;
                shake30 = shake50 = shake70 = shake90 = true;//恢复正常时使得下一次抖动能重置位置
                for (int i = phase.Length - 1; i >= 0; i--)
                {
                    phase[i] = false;
                }
            }
            else if (tolerateBar < 50)//>30
            {
                if (shake30)
                {
                    playerSpr.localPosition = shakePos = new Vector2(0, 0);
                    shake50 = false;
                    phase[0] = true;
                    phase[1] = false;//同阶段高状态回到低状态时，写回为低状态
                }
                shakePos.x += 0.05f * Mathf.Sin(shakeTime * 25);
                playerSpr.localPosition = shakePos;
            }
            else if (tolerateBar < 70)//>50
            {
                if (shake50)
                {
                    playerSpr.localPosition = shakePos = new Vector2(0, 0);
                    shake50 = false;
                    phase[1] = true;
                    phase[2] = false;//同阶段高状态回到低状态时，写回为低状态
                }
                shakePos.x += 0.1f * Mathf.Sin(shakeTime * 50);
                playerSpr.localPosition = shakePos;
            }
            else if (tolerateBar < 90)//>70
            {
                if (shake70)
                {
                    playerSpr.localPosition = shakePos = new Vector2(0, 0);
                    shake70 = false;
                    phase[2] = true;
                    phase[3] = false;//同阶段高状态回到低状态时，写回为低状态
                }
                shakePos.x += 0.1f * Mathf.Sin(shakeTime * 75);
                playerSpr.localPosition = shakePos;
            }
            else if (tolerateBar < 100)//>90
            {
                if (shake90)
                {
                    playerSpr.localPosition = shakePos = new Vector2(0, 0);
                    shake90 = false;
                    phase[3] = true;//没有更高的同阶段状态了
                }
                shakePos.x += 0.15f * Mathf.Sin(shakeTime * 100);
                playerSpr.localPosition = shakePos;
            }
            else if (tolerateBar > 100)
            {
                playerSpr.localPosition = new Vector2(0, 0);
                laughTrigger = true;
                if(laughCount==1)
                {
                    laughCount--;
                    GameManager.Instance.SwitchState(true);
                }
            }
        }
        if (tolerateBar > 100)
        {
            tolerateBar = 100;
        }

        //同时转变的动画切换
        if (laughTrigger)
        {
            for (int i = 0; i < phase.Length; i++)
            {
                phase[i] = true;
            }
        }
    }
    public void LifePhase()
    {
        if (laughTrigger)
        {
            unDead = false;
        }
        if (unDead || life > 10)
        {
            life = 10;
        }
        if (life <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        isDead = true;
    }
    #endregion


    #endregion
}
