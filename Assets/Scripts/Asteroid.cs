using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Rigidbody2D rbody;
    public Vector2 direction;
    public float speed;

    public int generation = 1;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        speed = Random.Range(.75f, 1.25f);
        speed *= generation;

        float randSize = Random.Range(2, 2.5f);
        float size = randSize / generation;
        transform.localScale = new Vector3(size, size, 1);

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
            GameManager.instance.AddToScore(50 * generation);
            generation++;
            GameManager.instance.RemoveAsteroid(gameObject);

            if (generation <= 3)
            {
                GameManager.instance.BreakAsteroids(generation, transform);
            }
            else
            {
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
    }
}