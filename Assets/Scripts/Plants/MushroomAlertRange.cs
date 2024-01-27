using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YSFramework.GlobalManager;

public class MushroomAlertRange : MonoBehaviour
{
    [SerializeField]
    private Mushroom body;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask(LayerName.Player))
            body.TriggerAttack();
    }
}
