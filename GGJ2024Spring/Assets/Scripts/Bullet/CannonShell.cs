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
        GetComponent<CircleCollider2D>().enabled = false;
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
        GetComponent<CircleCollider2D>().enabled = true;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        //Do nothing
    }
}
