using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIEffects;
using UnityEngine.UI;

public class HideEffect : MonoBehaviour
{
    [SerializeField]
    UITransitionEffect effect;
    [SerializeField]
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        effect.Hide();
        button.onClick.AddListener(()=> { Application.Quit(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
