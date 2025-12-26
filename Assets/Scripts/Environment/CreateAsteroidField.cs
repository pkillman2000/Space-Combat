using NUnit.Framework;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections.Generic;

public class CreateAsteroidField : MonoBehaviour
{
    [SerializeField]
    private float _width = 200;
    [SerializeField]
    private float _height = 200;
    [SerializeField]
    private List<GameObject> asteroidPrefabs;
    [SerializeField]
    private float _buffer = 2.0f;
    private List<GameObject> _asteroids = new List<GameObject>();
    private GameObject _asteroidPrefab;
    private Vector3 _currentPosition;
    private float _currentAsteroidSize;

    [SerializeField]
    private int _maxNumberOfRejections = 10;
    private int _currentRejections = 0;

    /*
    [SerializeField]
    private GameObject _backgroundGrid;
    private Renderer _backgroundGridRenderer;
    private Vector3 _planeSize;
    */
    
    private Renderer _renderer;
    
    void Start()
    {
    }

    void Update()
    {
        if (_currentRejections < _maxNumberOfRejections)
        {
            SelectAsteroidPrefab();
            CalculateRandomPosition();
            if (CanPlaceHere())
            {
                PlaceAsteroid();
            }
            else
            {
                _currentRejections++;
            }
        }
    }

    private bool CanPlaceHere()
    {
        foreach(GameObject asteroid in _asteroids)
        {
            float distance = Vector3.Distance(asteroid.transform.position, _currentPosition);
            if (distance < (CalculateAsteroidSize(asteroid) / 2) + (CalculateAsteroidSize(_asteroidPrefab) / 2) + _buffer)
            {
                _currentRejections++;
                return(false);
            }
        }
        return (true);
    }

    private void SelectAsteroidPrefab()
    {
        int index = Random.Range(0, asteroidPrefabs.Count);
        _asteroidPrefab = asteroidPrefabs[index];
    }

    private float CalculateAsteroidSize(GameObject asteroid)
    {
        _renderer = asteroid.GetComponent<Renderer>();
        if(_renderer == null)
        {
            Debug.LogError("Renderer component not found on the asteroid prefab.");
        }

        Vector3 size = _renderer.bounds.size;
        return((Mathf.Max(size.x, size.y, size.z)));
    }

    private void CalculateRandomPosition()
    {
        float x = Random.Range(-_width / 2, _width / 2);
        float z = Random.Range(-_height / 2, _height / 2);
        _currentPosition = new Vector3(x, 0, z) + transform.position;
    }

    private void PlaceAsteroid()
    {
        GameObject asteroid = Instantiate(_asteroidPrefab, _currentPosition, Quaternion.identity, this.transform);
        _asteroids.Add(asteroid);
    }
}