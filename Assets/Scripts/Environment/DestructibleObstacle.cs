using System.Collections.Generic;
using UnityEngine;

public class DestructibleObstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject _destructibleObstaclePrefab;
    [SerializeField]
    private GameObject _explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player Weapon"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Instantiate(_destructibleObstaclePrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
