using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static YSFramework.GlobalManager;

public class ToxicFumes : Projectile
{
    [SerializeField]
    private GameObject[] fumes;
    [SerializeField]
    private Sprite[] fumeSprites;
    private void RecycleProj(object[] args)
    {
        foreach (ToxicFumes childComp in transform.parent.GetComponentsInChildren<ToxicFumes>())
            StartCoroutine(Disappear(childComp.gameObject));
        if (isActiveAndEnabled)
            StartCoroutine(DelayRecycle());
    }
    public override void Launch(Vector2 start, Vector2 dir, float speed, params object[] values)
    {
        int maxDistance = 10;
        try
        {
            if (values != null && values.Length > 0)
                maxDistance = (int)values[0];
        }
        catch (UnityException e)
        {
            Debug.LogWarning(e.ToString());
        }

        string recycleProjTimerName = TimerInstance.CreateEventTimer("RecycleProj", 1, RecycleProj, null, true, false);
        TimerInstance.StartTimer(recycleProjTimerName);

        base.Launch(start, dir, speed);

        foreach (SpriteRenderer sp in GetComponentsInChildren<SpriteRenderer>())
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);
        }

        transform.rotation = Quaternion.identity;
        transform.Rotate(0, 0, Vector3.SignedAngle(Vector2.right, dir, Vector3.forward));
        StartCoroutine(Spread(dir, speed, maxDistance, 1));
    }
    private IEnumerator Spread(Vector2 dir,float speed, int maxSpreadingDistance, int spreadingCount)
    {
        GenerateParticle();
        if (spreadingCount >= maxSpreadingDistance)
            yield break;


        Vector2 next = (Vector2)transform.position + dir.normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(next, Vector2.zero);
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.layer == LayerMask.GetMask(LayerName.Wall))
                    yield break;
            }
        }

        yield return new WaitForSeconds(0.01f / speed);
        GameObject nextObj = ObjPool.RequestObject(gameObject);
        nextObj.transform.parent = transform.parent;
        nextObj.transform.position = next;
        nextObj.transform.rotation = Quaternion.identity;
        nextObj.transform.Rotate(0, 0, Vector3.SignedAngle(Vector2.right, dir, Vector3.forward));
        StartCoroutine(nextObj.GetComponent<ToxicFumes>().Spread(dir, speed, maxSpreadingDistance, spreadingCount + 1));

        void GenerateParticle()
        {
            foreach (GameObject go in fumes)
            {
                Color color = go.GetComponent<SpriteRenderer>().color;
                color.a = 1;
                go.GetComponent<SpriteRenderer>().color = color;
                if (Random.Range(1, 10) < 8)
                {
                    go.SetActive(true);
                    go.GetComponent<SpriteRenderer>().sprite = fumeSprites[Random.Range(0, 2)];
                    StartCoroutine(ParticleMove(go, dir));
                }
                else
                    go.SetActive(false);
            }
        }
    }
    private IEnumerator ParticleMove(GameObject particle, Vector2 dir)
    {
        particle.transform.localPosition = new Vector2(Random.Range(-0.5f, 0.2f), Random.Range(-0.35f, 0.35f));
        particle.transform.Rotate(0, 0, Random.Range(0f, 360f));
        float randomRotate = Random.Range(360, 1080);
        Quaternion rotationDes = particle.transform.localRotation * Quaternion.AngleAxis(randomRotate, Vector3.forward);
        Vector2 moveStart = (Vector2)particle.transform.position;
        Vector2 moveDes = moveStart + dir.normalized * Random.Range(0.4f, 1.8f);
        Vector2 currentVelocity = Vector2.zero;
        SpriteRenderer sp = particle.GetComponent<SpriteRenderer>();
        do
        {
            particle.transform.localRotation = Quaternion.Slerp(particle.transform.localRotation, rotationDes, 0.02f);
            particle.transform.position = Vector2.SmoothDamp(particle.transform.position, moveDes, ref currentVelocity, 0.8f);
            yield return null;
        } while ((moveDes - (Vector2)particle.transform.position).sqrMagnitude > 0.05f);
    }
    /// <summary>
    /// 令所有弹幕精灵图在0.5秒内渐变消失
    /// </summary>
    /// <param name="proj"></param>
    /// <returns></returns>
    private IEnumerator Disappear(GameObject proj)
    {
        float[] disappearTime = new float[5];
        float startTime = Time.time;
        for (int i = 0; i < 5; i++)
        {
            disappearTime[i] = Random.Range(0.2f, 0.4f);
        }
        SpriteRenderer sp;
        while ((Time.time - startTime) / 0.4f < 0.95)
        {
            for (int i = 0; i < 5; i++)
            {
                sp = proj.transform.GetChild(i).GetComponent<SpriteRenderer>();
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1 - ((Time.time - startTime) / disappearTime[i]));
            }
            yield return null;
        }
    }
    private IEnumerator DelayRecycle()
    {
        yield return new WaitForSeconds(0.9f);
        foreach (ToxicFumes childComp in transform.parent.GetComponentsInChildren<ToxicFumes>())
        {
            ObjPool.RecycleObject(childComp.gameObject);
        }
    }
    protected override void Update()
    {
        //Do nothing
    }
}
