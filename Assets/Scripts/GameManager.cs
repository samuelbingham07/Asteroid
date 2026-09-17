using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Animator anim;

    public static GameManager instance;

    public GameObject player;
    public GameObject asteroid;

    int asteroidsToSpawn = 5;
    public float asteroidPadding = 2;

    public List<GameObject> asteroidList = new();

    public int playerLives = 3;
    GameObject currentPlayer;

    public int score = 0;

    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI livesDisplay;
    private void Awake()
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
    void Start()
    {
        StartGame();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        scoreDisplay.text = "SCORE: " + score.ToString();
    }

    void SpawnPlayer()
    {
        currentPlayer = Instantiate(player, Vector3.zero, Quaternion.identity);
    }

    void SpawnAsteroids()
    {
        for (int i = 0; i < asteroidsToSpawn; i++)
        {
            GameObject newAsteroid = Instantiate(asteroid, GetRandomAsteroidPosition(), Quaternion.identity);
            AddAsteroid(newAsteroid);
        }
    }

    public void BreakAsteroids(int asteroidGeneration, Transform parentTransform)
    {
        for (int i = 0; i < asteroidGeneration; i++)
        {
            GameObject newAsteroid = Instantiate(asteroid, parentTransform.position, parentTransform.rotation);
            AddAsteroid(newAsteroid);
            newAsteroid.GetComponent<Asteroid>().generation = asteroidGeneration;
        }
    }

    void StartGame()
    {
        SpawnPlayer();
        SpawnAsteroids();
    }

    public void AddAsteroid(GameObject asteroid)
    {
        asteroidList.Add(asteroid);
    }

    public void RemoveAsteroid(GameObject asteroid)
    {
        asteroidList.Remove(asteroid);
        if (asteroidList.Count == 0)
        {
            asteroidsToSpawn++;
            SpawnAsteroids();
        }
    }

    Vector3 GetRandomAsteroidPosition()
    {
        float height = Camera.main.orthographicSize * 2 - 2;
        float width = (Camera.main.orthographicSize * 2 - 2) * Camera.main.aspect;
        Vector3 randomPosition = Vector3.zero;
        int randomZone = Random.Range(0, 4);

        if (randomZone == 0)
        {
            float randX = Random.Range(-width / 2, width / 2);
            float randY = Random.Range(height / 2, height / 2 - asteroidPadding);
            randomPosition = new Vector3(randX, randY, 0);
        }
        else if (randomZone == 1)
        {
            float randX = Random.Range(-width / 2, width / 2);
            float randY = Random.Range(-height / 2, -height / 2 + asteroidPadding);
            randomPosition = new Vector3(randX, randY, 0);
        }
        else if (randomZone == 2)
        {
            float randX = Random.Range(-width / 2, -width / 2 + asteroidPadding);
            float randY = Random.Range(-height / 2, height / 2);
            randomPosition = new Vector3(randX, randY, 0);
        }
        else if (randomZone == 3)
        {
            float randX = Random.Range(width / 2 - asteroidPadding, width / 2);
            float randY = Random.Range(-height / 2, height / 2);
            randomPosition = new Vector3(randX, randY, 0);
        }

        return randomPosition;
    }

    public void PlayerDeath()
    {
        playerLives--;
        livesDisplay.text = "LIVES: " + playerLives.ToString();


        if (playerLives == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Destroy(currentPlayer);
            SpawnPlayer();
        }
    }
}