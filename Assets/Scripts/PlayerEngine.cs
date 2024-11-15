using System;
using UnityEngine;

public class PlayerEngine : MonoBehaviour
{
    [SerializeField] private PlanetSpawner _planetSpawner;

    [SerializeField] private float _forwardSpeed = 3f;
    [SerializeField] private float _strafeSpeed = 5f;

    [SerializeField] private float _speedBooster = 1.05f;

    private Transform _transform;
    private Vector3 _targetPosition;
    private Vector3 _nextPosition;

    private float _accelerateSpeedFrequency = 1f;

    private readonly float _startAccelerateSpeed = 1f;

    public event Action GetBoost;

    public float StrafeSpeed => _strafeSpeed;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _targetPosition = _planetSpawner.GetTargetPosition();

        InvokeRepeating(nameof(AccelerateSpeed), _startAccelerateSpeed, _accelerateSpeedFrequency);
    }

    private void Update()
    {
        MoveForward();
    }

    public void UpBoost()
    {
        GetBoost?.Invoke();

        _targetPosition = _planetSpawner.GetTargetPosition();
    }

    private void MoveForward()
    {
        _nextPosition = new Vector3(_transform.position.x, Vector3.Distance(_transform.position, _nextPosition), _targetPosition.z);

        _transform.position = Vector3.MoveTowards(_transform.position, _nextPosition, _forwardSpeed * Time.deltaTime);
        //_transform.Translate(Vector3.forward * _forwardSpeed * Time.deltaTime);

        _transform.LookAt(_nextPosition);
    }

    private void AccelerateSpeed()
    {
        _forwardSpeed *= _speedBooster;
        _strafeSpeed *= _speedBooster;
    }
}
