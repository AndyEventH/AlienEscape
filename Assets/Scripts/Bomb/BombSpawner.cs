using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    public Tilemap tile;

    public GameObject bombPrefab;

    public GameObject player;
    public bool bombSpawned = false;

    void Update()
    {

        Vector3 worldPos = player.transform.position;
        worldPos.y = worldPos.y + 0.45f;
        Vector3Int cell = tile.WorldToCell(worldPos);

        if (Input.GetKeyDown(KeyCode.Space) && !bombSpawned)
        {
            SpawnBomb(cell);
        }
    }

    void SpawnBomb(Vector3Int cell)
    {
        
        Vector3 cellCenterPos = tile.GetCellCenterWorld(cell);

        Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
        bombSpawned = true;
    }


}
