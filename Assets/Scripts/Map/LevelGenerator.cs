using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap tileM; 

    [SerializeField] private Tile destructibleTile;

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private GameObject gatePrefab;

    public int numberOfTiles = 50;


    public float newEnemySpeed = 0;
    private GameManager GM;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        System.Random rnd = new System.Random();
        CreateDestructibleTiles(rnd);
        SpawnEnemies(rnd);
    }

    void CreateDestructibleTiles(System.Random rnd)
    {

        int tiles = 0;
        tileM.SetTile(new Vector3Int(-7, 1, 0), destructibleTile);
        tileM.SetTile(new Vector3Int(-9, -1, 0), destructibleTile);
        tiles = 2;
        bool gateAdded = false;
        while (tiles < numberOfTiles)
        {
            int x = rnd.Next(-9, 12);
            int y = rnd.Next(-9, 2);
            if ((x != -9 && x != -8) || (y != 1 && y != 0))
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (tileM.GetTile(pos) == null)
                {
                    if (!gateAdded)
                    {
                        Vector3 cellCenterPos = tileM.GetCellCenterWorld(pos);
                        Instantiate(gatePrefab, cellCenterPos, Quaternion.identity);
                        gateAdded = true;
                    }
                    tileM.SetTile(pos, destructibleTile);
                    tiles++;
                }
            }            
        }
    }

    void SpawnEnemies(System.Random rnd)
    {

        int enemies = 0;
        while (enemies < GM.numberOfEnemies)
        {
            int x = rnd.Next(-9, 12);
            int y = rnd.Next(-9, 2);
            if ((x != -9 && x != -8) || (y != 1 && y != 0))
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (tileM.GetTile(pos) == null)
                {
                    Vector3 cellCenterPos = tileM.GetCellCenterWorld(pos);
                    GameObject enemy = Instantiate(enemyPrefab, cellCenterPos, Quaternion.identity);
                    enemy.GetComponent<EnemyController>().tile = tileM;

                    enemies++;
                }
            }
        }
    }

}
