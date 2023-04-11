using UnityEngine;

public class Explosion : MonoBehaviour
{
    public new Collider2D collider;

    float collisionTime = 0.4f;

    void Update()
    {

        collisionTime -= Time.deltaTime;
        if (collisionTime <= 0f)
        {
            collider.enabled = false;
        }
        Destroy(gameObject, 0.9f);
    }

}
