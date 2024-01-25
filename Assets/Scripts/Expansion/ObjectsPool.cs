using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    /// <summary>
    /// ����tag���治ͬԤ����Ķ���ĳ���
    /// </summary>
    private Dictionary<string, Queue<GameObject>> pool;
    /// <summary>
    /// �洢��ע��Ķ����InstanceID��ע��Ķ�����ζ�����ڳ����л
    /// </summary>
    private Dictionary<GameObject, string> goTag;
    /// <summary>
    /// ������ն���ĸ�����
    /// </summary>
    private GameObject cachePanel;
    /// <summary>
    /// ������г�
    /// </summary>
    public void ClearCache()
    {
        pool.Clear();
        goTag.Clear();
    }
    /// <summary>
    /// ���ն���
    /// </summary>
    /// <param name="go">�����յĶ���</param>
    public void RecycleObject(GameObject go)
    {
        if (go == null)
            return;

        if (goTag.ContainsKey(go))
        {
            go.transform.parent = cachePanel.transform;
            go.SetActive(false);

            string tag = goTag[go];
            goTag.Remove(go);
            if (!pool.ContainsKey(tag))
            {
                pool.Add(tag, new Queue<GameObject>());
            }
            pool[tag].Enqueue(go);
        }
        else
        {
            Destroy(go);
        }
    }
    /// <summary>
    /// �ӳ����������
    /// </summary>
    /// <param name="prefab">Ҫ����Ķ����Ԥ����</param>
    /// <returns>Ԥ�����Ӧ�Ķ���</returns>
    public GameObject RequestObject(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is null");
            return null;
        }
        string tag = prefab.GetInstanceID().ToString();
        GameObject go;
        if (pool.ContainsKey(tag))
        {
            if (pool[tag].Count > 0)
            {
                go = pool[tag].Dequeue();
            }
            else
            {
                go = Instantiate(prefab);
            }
        }
        else
        {
            pool.Add(tag, new Queue<GameObject>());
            go = Instantiate(prefab);
        }
        goTag.Add(go, tag);
        go.transform.parent = null;
        go.SetActive(true);
        return go;
    }
    private void Awake()
    {
        pool = new Dictionary<string, Queue<GameObject>>();
        goTag = new Dictionary<GameObject, string>();
        if (cachePanel == null)
        {
            cachePanel = new GameObject("CachePanel");
            DontDestroyOnLoad(cachePanel);
        }
    }
}
