using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    /// <summary>
    /// 根据tag储存不同预制体的对象的池子
    /// </summary>
    private Dictionary<string, Queue<GameObject>> pool;
    /// <summary>
    /// 存储已注册的对象的InstanceID
    /// </summary>
    private Dictionary<GameObject, string> goTag;
    /// <summary>
    /// 缓存回收对象的父物体
    /// </summary>
    private GameObject chachePanel;
    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="go">被回收的对象</param>
    public void RecycleObject(GameObject go)
    {
        if (chachePanel == null)
        {
            chachePanel = new GameObject("ChachePanel");
            DontDestroyOnLoad(chachePanel);
        }

        if (go == null)
            return;

        if (goTag.ContainsKey(go))
        {
            go.transform.parent = chachePanel.transform;
            go.SetActive(false);

            string tag = goTag[go];
            goTag.Remove(go);
            if (!pool.ContainsKey(tag))
                pool.Add(tag, new Queue<GameObject>());
            pool[tag].Enqueue(go);
        }
        else
            Destroy(go);
    }
    /// <summary>
    /// 从池中请求对象
    /// </summary>
    /// <param name="prefab">要请求的对象的预制体</param>
    /// <returns>预制体对应的对象</returns>
    public GameObject RequestObject(GameObject prefab)
    {
        string tag = prefab.GetInstanceID().ToString();
        GameObject go;
        if (pool.ContainsKey(tag))
        {
            if (pool[tag].Count > 0)
                go = pool[tag].Dequeue();
            else
                go = Instantiate(prefab);
        }
        else
        {
            pool.Add(tag, new Queue<GameObject>());
            go = Instantiate(prefab);
        }
        goTag.Add(go, tag);
        go.transform.parent = null;
        return go;
    }
    private void Awake()
    {
        pool = new Dictionary<string, Queue<GameObject>>();
        goTag = new Dictionary<GameObject, string>();
    }
}
