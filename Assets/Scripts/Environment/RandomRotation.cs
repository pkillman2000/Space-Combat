using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [SerializeField]
    private float _minRotationSpeed = 5;
    [SerializeField]
    private float _maxRotationSpeed = 10;

    private float _xRotationSpeed;
    private float _yRotationSpeed;
    private float _zRotationSpeed;

    void Start()
    {
        _xRotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        _yRotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        _zRotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);

    }

    void Update()
    {
        transform.Rotate(_xRotationSpeed * Time.deltaTime, _yRotationSpeed * Time.deltaTime, _zRotationSpeed * Time.deltaTime);
    }
}
