using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public new SpriteRenderer renderer;

    public Collider2D boxCol;
    public Collider2D circleCol;

    public Sprite gateClosed;
    public Sprite gateOpen;
    private GameManager GM;
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }
    void Update()
    {

        if (GM.numberOfEnemies <= 0)
        {
            renderer.sprite = gateOpen;
            boxCol.enabled = false;
            circleCol.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag.Equals("Player"))
        {
            GM.isWin = 1;
        }
    }
}
