using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float delay = 2f;
    BombSpawner bomb;
    MapDestroyer map;
    [SerializeField] private CapsuleCollider2D cc2d;
    private void Start()
    {
        bomb = FindObjectOfType<BombSpawner>();
        map = FindObjectOfType<MapDestroyer>();
    }
    private void Update()
    {

        delay -= Time.deltaTime;

        if (delay <= 0f)
        {
            if (map)
                map.Explosion(transform.position);
            Destroy(gameObject);
            if (bomb)
                bomb.bombSpawned = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cc2d.enabled= true;
        }

    }
}
