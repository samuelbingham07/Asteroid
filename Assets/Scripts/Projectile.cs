using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rBody;
    public float speed = 15f;
    bool forcedAdded = false;
    public float lifetime = .5f;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (forcedAdded == false)
        {
            rBody.AddForce(transform.up * speed, ForceMode2D.Impulse);
            forcedAdded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}