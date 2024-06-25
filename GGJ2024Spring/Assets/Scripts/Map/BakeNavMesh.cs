using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

public class BakeNavMesh : MonoBehaviour
{
    #region Variables
    NavMeshSurface navMeshSurface;
    #endregion

    #region Functions
    // Awake
    void Awake()
    {
        init();
        BakeNavMeshAwake();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BakeNavMeshUpdate();
    }

    //Custom Func
    void init()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    void BakeNavMeshAwake()
    {
        navMeshSurface.BuildNavMesh();
    }

    void BakeNavMeshUpdate()
    {
        navMeshSurface.RemoveData();
        navMeshSurface.BuildNavMesh();
    }
    #endregion
}
