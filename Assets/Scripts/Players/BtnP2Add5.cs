using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnP2Add5 : MonoBehaviour
{
    Button btnP2;
    // Start is called before the first frame update
    void Start()
    {
        btnP2 = GetComponent<Button>();
        //btnP2.onClick.AddListener(BtnClkP2);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BtnClkP2()
    {
        P2Tolerate.tolerateBarP2 += 5;
        Debug.Log("P2来点笑话5");
    }
}
