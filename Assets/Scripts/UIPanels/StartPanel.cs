using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Coffee.UIEffects;
using UnityEngine.SceneManagement;

public class StartPanel : BasePanel
{
    [SerializeField]
    Button ExitBtn;
    [SerializeField]
    UITransitionEffect effect;

    bool isEffect;
    float effectSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        ExitBtn.onClick.AddListener(() => { Application.Quit();});
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            effect.Show();
            StartCoroutine(LoadScene());
        }

       
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(effect.effectPlayer.duration+2f);
        SceneManager.LoadScene("Main");
    }
   
}
