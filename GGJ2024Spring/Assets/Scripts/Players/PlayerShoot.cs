using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    // Start is called before the first frame update
    //SF
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    public float coolDownLimit;

    //NoSF
    [SerializeField]
    private Transform shootingFace;
    public float shootCoolDown;
    public bool shootable;
    [SerializeField]
    public PlayerState state;
    bool isShoot;

    private float hitCooldown;
    private bool canHit;
    protected void Start()
    {
        shootCoolDown = 1;
        shootable = false;
        canHit = true;
        state=gameObject.GetComponent<PlayerState>();
    }

    // Update is called once per frame
    protected void Update()
    {
        shootCoolDown += Time.deltaTime;
        TrigOrShoot();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer==16);
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == 16)
        {
            if(!state.isDead)
            {
                if (PlayerState.laughTrigger)
                {
                    if (shootable)
                    {
                        Shoot();
                        shootable = false;
                        shootCoolDown = 0;
                    }
                }
                else
                {
                    if (shootable)
                    {
                        state.tolerateBar += 20;
                        shootable = false;
                        shootCoolDown = 0;
                    }
                }
            }
        }
        else if(PlayerState.laughTrigger && canHit && collision.gameObject.layer == 18)
        {
            state.life -= 1;
            StartCoroutine(HitCooldown());
        }

        IEnumerator HitCooldown()
        {
            canHit = false;
            yield return new WaitForSeconds(1.5f);
            canHit = true;
        }
    }



    public void Shoot()
    {
        Debug.Log("BOOm");
        float bulletAngle = Mathf.Atan2(shootingFace.localPosition.y, shootingFace.localPosition.x);
        //biu
        Quaternion rotation = Quaternion.Euler(0, 0, bulletAngle * Mathf.Rad2Deg);
        GameObject newBullet = Instantiate(bullet, transform.position, rotation);
        newBullet.GetComponent<Projectile>().Launch(transform.position, new Vector2(shootingFace.localPosition.x, shootingFace.localPosition.y), 2f);

        Vector2 RotateVector(Vector2 vector, float angle)
        {
            float radianAngle = angle * Mathf.Deg2Rad;

            float sinValue = Mathf.Sin(radianAngle);
            float cosValue = Mathf.Cos(radianAngle);

            float rotatedX = vector.x * cosValue - vector.y * sinValue;
            float rotatedY = vector.x * sinValue + vector.y * cosValue;

            return new Vector2(rotatedX, rotatedY);
        }
    }


    public void TrigOrShoot()
    {
        if (shootCoolDown > coolDownLimit)
        {
            shootable = true;
        }
        else
        {
            shootable = false;
        }

    }

}
