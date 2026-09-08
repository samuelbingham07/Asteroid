using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D rbody;
    public Vector2 direction = Vector2.right;
    public float speed = 1f;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        speed = Random.Range(1.5f, 2.5f);
        direction = Random.insideUnitCircle;
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rbody.position + direction.normalized * Time.fixedDeltaTime * speed;
        rbody.MovePosition(newPosition);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
