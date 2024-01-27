using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShell : Projectile
{
    [SerializeField]
    private Animator animator;
    public override void Launch(Vector2 start, Vector2 _, float speed, params object[] des)
    {
        transform.position = start;
        StartCoroutine(Fly());

        IEnumerator Fly()
        {
            Vector3 destination = (Vector3)des[0];
            GetComponent<ProjectileUpward>().MoveInParabola(start, destination);
            yield break;
        }
    }
    public void Boom()
    {
        //Booooooooooooooooooooom!
        rb.velocity = Vector3.zero;
        animator.SetTrigger("Blast");
        Collider2D col = Physics2D.OverlapCircle(transform.position, 0.55f, LayerMask.GetMask(LayerName.Player));
        if (col)
        {
            //Íæ¼ÒÊÜ»÷
        }
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
