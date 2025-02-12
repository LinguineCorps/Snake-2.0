using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private Vector2 _direction = Vector2.right;

    public float SnakeSpeed = 0.08f;

    public float SnakeDecayTime = 0.7f;

    public int StartSnakeSize = 5;

    public int SnakeAddSize = 5;

    private List<Transform> _segments;

    public Transform segmentPrefab;

    public GameObject GameOverMenu;


    void Awake()
    {
        Time.fixedDeltaTime = SnakeSpeed;
    }

    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);

        StartCoroutine(RemoveSegmentEverySecond());

        for (int i = 0; i < StartSnakeSize; i++)
        {
            Transform segment = Instantiate(segmentPrefab);
            _segments.Add(segment);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }
    }
    private void grow()
    {
        if (_segments.Count < StartSnakeSize)
        {
            for (int i = 0; i < SnakeAddSize; i++)
            {
                if(_segments.Count == StartSnakeSize)
                {
                    break;
                }
                else
                {
                    Transform segment = Instantiate(this.segmentPrefab);
                    segment.position = _segments[_segments.Count - 1].position;
                    _segments.Add(segment); // Add the new segment to the list
                }
                
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            grow();
        }

        else if (other.tag == "Wall")
        {
            Time.timeScale = 0.0f;
            GameOverMenu.SetActive(true);
        }
    }



    IEnumerator RemoveSegmentEverySecond()
    {
        while (_segments.Count > 0)
        {
            // Wait for one second
            yield return new WaitForSeconds(SnakeDecayTime);

            // Remove the last segment
            int lastIndex = _segments.Count - 1;
            Destroy(_segments[lastIndex].gameObject);
            _segments.RemoveAt(lastIndex);
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--) 
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + _direction.x, Mathf.Round(this.transform.position.y) + _direction.y, 0.0f);
    }
}
