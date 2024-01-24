using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarP1 : MonoBehaviour
{
    Stack<Transform> healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = new Stack<Transform>();
        for (int i = 0; i < 10; i++)
        {
            healthBar.Push(transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealthCTRL();
    }

    /// <summary>
    /// 用栈来实现血量的逐个扣减
    /// 如果扣血，则出栈并把出栈元素的图片改成空血槽
    /// 如果加血，则将空血槽的图片改成满血槽并入栈
    /// </summary>
    void HealthCTRL()
    {
        while (P1Life.lifeP1 < healthBar.Count)//扣血
        {
            Transform emptyHP = healthBar.Pop();
            UnityEngine.UI.Image hpImg = emptyHP.GetComponent<UnityEngine.UI.Image>();
            hpImg.sprite = Resources.Load<Sprite>("Images/UI/P1Damage");
        }
        while (P1Life.lifeP1 > healthBar.Count)//加血
        {
            int currHP = healthBar.Count;
            Transform fullHP = transform.GetChild(currHP);
            UnityEngine.UI.Image fullImg = fullHP.GetComponent<UnityEngine.UI.Image>();
            fullImg.sprite = Resources.Load<Sprite>("Images/UI/P1Health");
            healthBar.Push(fullHP);
        }
    }
}
