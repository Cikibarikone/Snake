using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldSnake : MonoBehaviour 
{

    public Vector2 _direction = Vector2.right;
    public List<Transform> _segments;
    public Transform segmentsPrefab;
    public OldFood food;
    [SerializeField] public int foodPickups = 0;

    public float initialMoveSpeed = 1f;
    public float moveSpeed;

    public void Start()
    {        
        moveSpeed = initialMoveSpeed;
        _segments = new List<Transform>();
        _segments.Add(this.transform);
        food.RandomizedPosition();        
    }

    private void Update()
    {
        // Restricts movement in opposite direction when there are 3 or more segments
        if (_segments.Count >= 3)
        {
            if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down)
            {
                _direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up)
            {
                _direction = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right)
            {
                _direction = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left)
            {
                _direction = Vector2.right;
            }
        }
        else
        {
            // Allow movement in any direction when there are less than 3 segments
            if (Input.GetKeyDown(KeyCode.W))
            {
                _direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _direction = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                _direction = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _direction = Vector2.right;
            }
        }
    }

    public void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--) 
        {
            _segments[i].position = _segments[i - 1].position;
        }

        float moveDistance = moveSpeed * Time.deltaTime;
        this.transform.position += (Vector3)(_direction * moveDistance);

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x + _direction.x), Mathf.Round(this.transform.position.y + _direction.y), 0.0f);

    }

    public void Grow()
    {               
        Transform segment = Instantiate(this.segmentsPrefab);
        segment.position = _segments[_segments.Count - 1].position;
                
        _segments.Add(segment);              

        IncreaseMoveSpeed();
    }

    public void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;

        ResetMoveSpeed();        
    }

    public void IncreaseMoveSpeed()
    {
        moveSpeed = initialMoveSpeed;
    }

    private void ResetMoveSpeed()
    {
        moveSpeed = initialMoveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();            
            food.RandomizedPosition();
            FoodPickups.instance.AddPoint();
        }
        else if (other.tag == "Obsticle")
        {
            ResetState();
            food.RandomizedPosition();
            FoodPickups.instance.ResetScore();
        }
    }
}

