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
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    //随机选择玩家进行追踪和射击
    public void ChoosePlayer()
    {
        int random = Random.Range(0, 99);
        target = random % 2 == 0 ? GameObject.Find("Player1").transform : GameObject.Find("Player2").transform;
    }
}
