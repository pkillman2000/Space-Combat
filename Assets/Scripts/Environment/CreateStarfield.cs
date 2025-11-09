using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStarfield : MonoBehaviour
{
    [Header("Boundaries")]
    [SerializeField]
    private float _minX;
    [SerializeField]
    private float _maxX;
    [SerializeField]
    private float _minZ;
    [SerializeField]
    private float _maxZ;

    [Header("Stars")]
    [SerializeField]
    private GameObject _star;
    [SerializeField]
    [Range(0, 1000)]
    private int _numberOfStarsToSpawn = 400;

    [Header("Gas Clouds")]
    [SerializeField]
    private GameObject _gasCloud;
    [SerializeField]
    [Range(0, 1000)]
    private int _numberOfGasCloudsToSpawn = 50;


    void Start()
    {
        SpawnStars();
        SpawnGasClouds();
    }

    private Vector3 GetSpawnLocation()
    {
        float x = Random.Range(_minX, _maxX);
        float z = Random.Range(_minZ, _maxZ);
        return (new Vector3(x, 0, z));

    }

    private void SpawnStars()
    {
        for (int i = 0; i < _numberOfStarsToSpawn; i++)
        {
            GameObject instantiatePrefab = Instantiate(_star, this.gameObject.transform);
            instantiatePrefab.transform.position = GetSpawnLocation();
        }
    }

    private void SpawnGasClouds()
    {
        for (int i = 0; i < _numberOfGasCloudsToSpawn; i++)
        {
            GameObject instantiatePrefab = Instantiate(_gasCloud, this.gameObject.transform);
            instantiatePrefab.transform.position = GetSpawnLocation();
        }
    }

}
