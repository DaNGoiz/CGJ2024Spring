using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YSFramework;

public class PlayerUI : MonoBehaviour
{
    int UIChangeCount;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Awake()
    {
        EventCenter.AddListener(EventCode.SwitchAction, UIAppear);
    }
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Init()
    {
        UIChangeCount = 1;
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 400);
    }
    void UIAppear()
    {
        StartCoroutine(Appear());
    }
    IEnumerator Appear()
    {
        if (UIChangeCount == 1)
        {
            UIChangeCount = 0;
            float T = 0;
            while (true)
            {
                T += Time.deltaTime;
                float present = T / 2;
                rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, Vector2.zero, present);
                if (present >= 1)
                {
                    break;
                }
                yield return null;
            }
        }
    }
}
