using UnityEngine;

public class TrapProjectile : MonoBehaviour
{
    private float speed;
    private float lifetime;
    private Vector3 dir;
    public void Setup(float speed, float lifetime, Vector3 dir)
    {
        this.speed = speed;
        this.lifetime = lifetime;
        this.dir = dir;

        float angle = Mathf.Atan2(this.dir.y, this.dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"NAme : {collision.gameObject.name}");

        if(collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.Death();
        }

        Destroy(gameObject);
    }
}