using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unguided : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20f;
    [SerializeField]
    private float _damage = 10f;
    [SerializeField]
    private float _lifeTime = 2f;
    private float _timer;

    /*
     * OnEnable is called because this object is being reused from an object pool.
    */
    private void OnEnable()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * _speed;
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _lifeTime)
        {
            MoveToObjectPool();
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void SetLifeTime(float lifeTime)
    {
        _lifeTime = lifeTime;
    }

    private void MoveToObjectPool()
    {
        this.transform.position = new Vector3(0, 1, -600);
        this.gameObject.SetActive(false);
    }
}
