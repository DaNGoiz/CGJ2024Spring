using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 生成单个房间，包括地板、墙壁、装饰物等
/// </summary>
public class GenerateSingleRoom : MonoBehaviour
{

    [Header("Tiles")]
    public Tile[] tiles;
    private Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Tilemap>().ClearAllTiles();
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
