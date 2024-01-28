using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FindRoad : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChoosePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    //随机选择玩家进行追踪和射击
    void ChoosePlayer()
    {
        int random = Random.Range(0, 99);
        if (random < 50)
        {
            target = GameObject.Find("Player1").transform;
        }
        else
        {
            target = GameObject.Find("Player2").transform;
        }
    }
}
