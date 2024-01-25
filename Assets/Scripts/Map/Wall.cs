using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    bool isNaturalWall;
    Door.Direction direction;

    public void ActivateBanner()
    {
        // if is not natural: show banner and collider
        if (!isNaturalWall)
        {

        }
        // else: show collider only
        else
        {

        }

    }

    public void DisactivateBanner()
    {
        // if is not natural: hide banner and collider
        if (!isNaturalWall)
        {
            
        }
    }
}
