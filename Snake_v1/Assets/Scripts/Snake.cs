using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private Vector2 nextDirection = Vector2.right;
    private List<Transform> segments = new List<Transform>();
    [SerializeField] private Transform segmentPrefab;
    [SerializeField] private Food food;
    [SerializeField] private ScoreManager scoreManager;
    private float speedIncrease = 0.005f;
    private float originalFixedDeltaTime;

    private void Start()
    {
        originalFixedDeltaTime = Time.fixedDeltaTime;
        DefaultGameState();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (segments.Count < 3)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                nextDirection = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                nextDirection = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                nextDirection = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                nextDirection = Vector2.right;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
            {
                nextDirection = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
            {
                nextDirection = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
            {
                nextDirection = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
            {
                nextDirection = Vector2.right;
            }
        }
    }


    private void FixedUpdate()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        direction = nextDirection;
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y, 0.0f);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SnakeScene")
        {
            Time.fixedDeltaTime = originalFixedDeltaTime;
        }
    }

    private void DefaultGameState()
    {
        scoreManager.ResetScore();
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(this.transform);

        this.transform.position = Vector3.zero;
        direction = Vector2.right;

        food.RandomizeFoodPosition();
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
        if (GameManager.Instance != null)
        {
            if (segments.Count > 1 && segments.Count % GameManager.Instance.DifficultyMod == 0)
            {
                Time.fixedDeltaTime -= speedIncrease;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
            scoreManager.AddPoints();

        }
        else if (other.tag == "Obsticle")
        {
            GameOver();
        }
    }
}
