using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    [SerializeField]
    Button returnBtn;
    // Start is called before the first frame update
    void Start()
    {
        returnBtn.onClick.AddListener(OnExit);
    }

    public override void OnEnter(object data)
    {
        Time.timeScale = 0;
        base.OnEnter(data);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnExit()
    {
        Time.timeScale = 1;
    }
}
