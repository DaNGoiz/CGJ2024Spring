using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnP1Add10 : MonoBehaviour
{
    Button btnP1;
    // Start is called before the first frame update
    void Start()
    {
        btnP1=GetComponent<Button>();
        //btnP1.onClick.AddListener(BtnClkP1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BtnClkP1()
    {
        P1Tolerate.tolerateBarP1 += 10;
        Debug.Log("P1来点笑话10");
    }
}
