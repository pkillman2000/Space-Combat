using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MinePursuing : MonoBehaviour
{
    private GameObject _player;

    [SerializeField]
    private float _detectionRange = 60f;
    [SerializeField]
    private float _rotationSpeed = 40f;
    [SerializeField]
    private float _followSpeed = 10f;
    [SerializeField]
    private GameObject _explosion;

    private bool _isFollowing = false;
    private Rigidbody _rb;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if ((_player == null))
        {
            Debug.LogWarning("Player not Found!");
        }

        // Get Rigidbody component (add if missing)
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody>();
        }

        // Configure Rigidbody for follower behavior
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        // Calculate distance to _player
        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        // Check if _player is within detection range
        if (distanceToPlayer <= _detectionRange)
        {
            // Player is detected, start following if not already
            if (!_isFollowing)
            {
                _isFollowing = true;
            }

            RotateTowardsPlayer();
            FollowPlayer();
        }
        else
        {
            // Player is out of range, stop following
            if (_isFollowing)
            {
                _isFollowing = false;
                StopFollowing();
            }
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        float playerAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion playerRotation = Quaternion.Euler(0, playerAngle, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, _rotationSpeed * Time.deltaTime);
    }

    private void FollowPlayer()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        Vector3 movement = direction * _followSpeed * Time.deltaTime;

        transform.position += movement;
    }

    private void StopFollowing()
    {
        // Stop any momentum
        if (_rb != null)
        {
            _rb.linearVelocity = Vector3.zero;
        }
    }

    // The mine will explode on contact with any collider
    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }

    private void Explode()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}