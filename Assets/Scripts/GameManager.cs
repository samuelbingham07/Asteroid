using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameManager instance;

    public GameObject player;
    public GameObject asteroid;

    int asteroidsToSpawn = 5;

    public List<GameObject> asteroidsList = new();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnPlayer()
    {
        Instantiate(player, Vector3.zero, Quaternion.identity);
    }

    void SpawnAsteroids()
    {
        for (int x = 0; x < asteroidsToSpawn; x++)
        {
            GameObject newAsteroid = Instantiate(asteroid, randomAsteroidPosition(), Quaternion.identity);
            AddAsteroid(newAsteroid);
        }

    }

    void StartGame()
    {
        SpawnAsteroids();
        SpawnPlayer();
    }

    Vector3 randomAsteroidPosition()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = Camera.main.orthographicSize * 2 * Camera.main.aspect;

        float randX = Random.Range(-width / 2, width / 2);
        float randY = Random.Range(-height / 2, height / 2);

        Vector3 randPos = new Vector3(randX, randY, 0);

        return randPos;
    }

    public void AddAsteroid(GameObject asteroid)
    {
        asteroidsList.Add(asteroid);
    }

    public void RemoveAsteroid(GameObject g)
    {
        asteroidsList.Remove(g);
        if(asteroidsList.Count == 0)
        {
            asteroidsToSpawn++;
            SpawnAsteroids();
        }
    }
}
