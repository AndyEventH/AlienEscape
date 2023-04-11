using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    public Tilemap tile;

    [SerializeField] private Animator animator;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private new Collider2D collider;

    [SerializeField] private GameObject[] wallChecks; 
    int[,] directions = new int[,] { { 0, -1 }, { 0, 1 }, { 1, 0}, { -1, 0} }; 

    bool directionChosen = false;

    int index;

    Vector2 movement;
    GameManager GM;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        Physics2D.IgnoreLayerCollision(8, 8);
    }
    void Update()
    {

        if (!directionChosen) 
        {
            GetDirection();
        }
        if (ObstacleThere(wallChecks[index])) {
            movement.x = 0;
            movement.y = 0;
            directionChosen = false;
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);      
    }

    void FixedUpdate()
    {
        ///Enemy Movement

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void GetDirection()
    {

        System.Random rnd = new System.Random();
        index = rnd.Next(4);
        while (ObstacleThere(wallChecks[index]))
        {
            index = rnd.Next(4);
        }
        movement.x = directions[index, 0];
        movement.y = directions[index, 1];
        movement.Normalize();
        directionChosen = true;
    }

    bool ObstacleThere(GameObject check)
    {

        Vector3 worldPos = check.transform.position;
        Vector3Int cell = tile.WorldToCell(worldPos);
        if (tile.GetTile(cell) != null) 
        {
            return true;
        }

        try
        {
            Vector3 bombPos = GameObject.FindGameObjectWithTag("Bomb").transform.position;
            Vector3Int bombCell = tile.WorldToCell(bombPos);
            if (cell == bombCell) 
            {
                return true;
            }
            Vector3 gatePos = GameObject.FindGameObjectWithTag("Gate").transform.position;
            Vector3Int gateCell = tile.WorldToCell(gatePos);
            if (cell == gateCell) 
            {
                return true;
            }
        }
        catch (System.NullReferenceException) 
        {
        }
        return false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag.Equals("Explosion"))
        {
            animator.SetBool("Death", true);
            moveSpeed = 0f;
            collider.enabled = false;
            GM.numberOfEnemies--;
            Destroy(gameObject, 2f);
        }
    }
}
