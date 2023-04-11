using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;



    [SerializeField] private Collider2D[] colliders;

    Vector2 input;

    GameManager GM;
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (input * speed * Time.deltaTime));
    }

    private void GetInput()
    {

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Explosion"))
        {
            speed = 0f;
            for (int i = 0; i < 3; i++)
            {
                colliders[i].enabled = false;
            }
            GM.isWin = 0;
            Destroy(gameObject, 2f);

        }

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            //Debug.Log("dead");
            speed = 0f;
            for (int i = 0; i < 3; i++)
            {
                colliders[i].enabled = false;
            }
            GM.isWin = 0;
            Destroy(gameObject, 2f);
        }

    }
}
