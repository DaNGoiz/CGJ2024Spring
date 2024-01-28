using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIEffects;

public class HideEffect : MonoBehaviour
{
    [SerializeField]
    UITransitionEffect effect;
    // Start is called before the first frame update
    void Start()
    {
        effect.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
