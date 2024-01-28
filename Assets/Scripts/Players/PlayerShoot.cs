using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Start is called before the first frame update
    //SF
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    public float coolDownLimit;

    //NoSF
    private Transform shootingFace;
    public float shootCoolDown;
    public bool shootable;


    protected void Start()
    {
        shootingFace = transform.GetChild(2);
        shootCoolDown = 0;
        shootable = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        shootCoolDown += Time.deltaTime;
        
    }
    public void Shoot()
    {
        {
            //计算发射方向
            float bulletAngle = Mathf.Atan2(shootingFace.localPosition.x, shootingFace.localPosition.y);
            //biu
            GameObject newBullet = Instantiate(bullet, transform.position, shootingFace.rotation);
            newBullet.transform.eulerAngles=new Vector3(0,0,bulletAngle*Mathf.Rad2Deg-90);
            Rigidbody2D newBulletRigid = newBullet.GetComponent<Rigidbody2D>();
            newBulletRigid.AddForce(shootingFace.localPosition*5,ForceMode2D.Impulse);
        }
    }

    public virtual void TrigOrShoot(){}
}
