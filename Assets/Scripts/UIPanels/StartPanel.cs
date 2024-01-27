using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartPanel : BasePanel
{
    [SerializeField]
    Button startBtn;
    [SerializeField]
    Button ExitBtn;
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(() => { SceneManager.LoadScene("Main"); });
        startBtn.onClick.AddListener(() => { Application.Quit(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
