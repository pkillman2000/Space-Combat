using UnityEngine;
using System;

[Serializable]
public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject _obstaclePrefab;
    [SerializeField]
    private int _obstacleWeight;

    public GameObject GetObstaclePrefab()
    {
        return _obstaclePrefab;
    }

    public int GetObstacleWeight()
    {
        return _obstacleWeight;
    }

    public void SetObstacleWeight(int weight)
    {
        _obstacleWeight = weight;
    }
}
