using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    [SerializeField] private Tilemap tileM;

    [SerializeField] private Tile destructibleTile;
    [SerializeField] private Tile solidTile;

    [SerializeField] private GameObject explosionPrefab;

    private void Start()
    {
    }
    public void Explosion(Vector2 pos)
    {

        Vector3Int baseCell = tileM.WorldToCell(pos);

        ExplodeCell(baseCell);
        ExplodeCell(baseCell + new Vector3Int(1, 0, 0));
        ExplodeCell(baseCell + new Vector3Int(-1, 0, 0));
        ExplodeCell(baseCell + new Vector3Int(0, 1, 0));
        ExplodeCell(baseCell + new Vector3Int(0, -1, 0));
    }

    void ExplodeCell(Vector3Int cell)
    {

        Tile tile = tileM.GetTile<Tile>(cell);

        if (tile != null && tile != destructibleTile)
        {
            return;
        }
        else if (tile == destructibleTile)
        {
            tileM.SetTile(cell, null);
        }

        Vector3 pos = tileM.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);
    }
}
