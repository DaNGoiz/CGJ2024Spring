using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float MoveSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
    }

    void CameraMove()
    {
        if(this.transform.position.x<=2 && this.transform.position.x>=-2)
        {
            this.gameObject.transform.Translate(Vector3.left*MoveSpeed*Time.deltaTime*Input.GetAxis("Mouse X"));
        }else
        {
            if(this.transform.position.x>0.1)
            {
                this.transform.position = new Vector3(2, 0, -10);
            }
            if (this.transform.position.x < -0.1)
            {
                this.transform.position = new Vector3(-2, 0, -10);
            }
        }
    }
}
