using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float screenWidth;
    float screenHeight;
    Rigidbody2D rBody;
    public float wrapMargin = .5f;
    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        screenHeight = Camera.main.orthographicSize * 2 + wrapMargin;
        screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect + wrapMargin;

        float heightEdge = screenHeight / 2;
        float widthEdge = screenWidth / 2;
        Vector2 position = rBody.position;

        if (position.y > heightEdge)
        {
            position.y = -heightEdge;
        }
        if (position.y < -heightEdge)
        {
            position.y = heightEdge;
        }
        if (position.x > widthEdge)
        {
            position.x = -widthEdge;
        }
        if (position.x < -widthEdge)
        {
            position.x = widthEdge;
        }

        rBody.position = position;
    }
}
