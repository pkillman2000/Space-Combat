using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Spawner : MonoBehaviour
{
    [Header("Obstacle Spawning Settings")]
    [SerializeField]
    private List<Obstacle> _obstacles;
    private int _totalObstacleWeight;
    [SerializeField]
    private int _totalObstacleCount;

    [Header("Enemy Spawning Settings")]
    [SerializeField]
    private List<EnemyWeight> _enemies;
    private int _totalEnemyWeight;
    [SerializeField]
    private int _totalEnemyCount;

    [Header("Stars Spawning Settings")]
    [SerializeField]
    private GameObject _star;
    [SerializeField]
    private int _totalStarCount;

    [Header("Spawn Area Settings")]
    [SerializeField]
    private Vector2 _minMaxX;
    [SerializeField]
    private Vector2 _minMaxZ;

    private void Start()
    {
        // Calculate total weight
        _totalObstacleWeight = 0;
        foreach (var obstacle in _obstacles)
        {
            _totalObstacleWeight += obstacle.GetObstacleWeight();
        }

        if(_totalObstacleWeight > 0)
        {
            SpawnObstacles(_totalObstacleCount);
        }
    }

    private void SpawnObstacles(int numberToSpawn)
    {
        int randomWeight;
        int currentWeight;

        for (int i = 0; i < numberToSpawn; i++)
        {
            randomWeight = UnityEngine.Random.Range(0, _totalObstacleWeight);
            currentWeight = 0;

            foreach (var obstacle in _obstacles)
            {
                currentWeight += obstacle.GetObstacleWeight();
                if (randomWeight < currentWeight)
                {
                    Instantiate(obstacle.GetObstaclePrefab(), GetRandomPosition(), Quaternion.identity);
                    break;
                }
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        int randomX = UnityEngine.Random.Range((int)_minMaxX.x, (int)_minMaxX.y);
        int randomZ = UnityEngine.Random.Range((int)_minMaxZ.x, (int)_minMaxZ.y);
        return new Vector3(randomX, 0, randomZ);
    }
}
