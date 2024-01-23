using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    //SF
    [SerializeField]
    GameObject bullet;

    //NoSF
    private Transform shootingFace;
    
    void Start()
    {
        shootingFace = transform.GetChild(2);
    }

    // Update is called once per frame
    void Update()
    {
        //计算发射方向
        float bulletAngle = Mathf.Atan2(shootingFace.localPosition.x,shootingFace.localPosition.y);
        if(Player1CTRL.laughTriggerP1)
        {
            // if(Input.GetKeyDown(KeyCode.G)||Input.GetKeyDown(KeyCode.H))//biu~
            // {
            //     GameObject newBullet = Instantiate(bullet, transform.position, shootingFace.rotation);
            //     newBullet.transform.eulerAngles=new Vector3(0,0,bulletAngle*Mathf.Rad2Deg-90);
            //     Rigidbody2D newBulletRigid = newBullet.GetComponent<Rigidbody2D>();
            //     newBulletRigid.AddForce(shootingFace.localPosition*5,ForceMode2D.Impulse);
            // }

        }
    }
}
