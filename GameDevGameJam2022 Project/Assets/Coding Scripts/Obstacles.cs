using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Obstacles : MonoBehaviour
{
    Tilemap tilemap;
    [SerializeField] private GameObject obstaclePrefab;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        SpawnObstacles();
    }

    private void SpawnObstacles()
    {
        for (int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
        {
            for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
            {
                Vector3Int checkVector = new Vector3Int(x,y,0);

                if (tilemap.HasTile(checkVector))
                {
                    GameObject newObstacle = Instantiate(obstaclePrefab, checkVector, Quaternion.identity);
                    newObstacle.transform.parent = gameObject.transform;
                }
            }
        }
    }

    
}
