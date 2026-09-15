using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Rigidbody2D rbody;
    public Vector2 direction;
    public float speed;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        speed = Random.Range(.75f, 1.25f);
        direction = Random.insideUnitCircle;
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rbody.position + direction.normalized * Time.fixedDeltaTime * speed;
        rbody.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            GameManager.instance.RemoveAsteroid(gameObject);
            Destroy(gameObject);
        }
    }

}