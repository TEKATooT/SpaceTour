using System;
using UnityEngine;

public class PlayerEngine : MonoBehaviour
{
    [SerializeField] private PlanetSpawner _planetSpawner;

    [SerializeField] private float _forwardSpeed = 3f;
    [SerializeField] private float _strafeSpeed = 5f;
    [SerializeField] private float _speedBooster = 1.05f;
    [SerializeField] private ParticleSystem _particleSystem;

    static private float _accelerateSpeedFrequency;

    private ParticleSystem.MainModule _mainParticleSystem;
    private Transform _transform;
    private Vector3 _targetPosition;
    private Vector3 _deadLine;

    public event Action GetBoost;
    public event Action LoseBoost;

    private float _deadHight = 0.5f;
    private readonly float _startAccelerateSpeed = 1f;

    public float StrafeSpeed => _strafeSpeed;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _mainParticleSystem = _particleSystem.main;

        if (_accelerateSpeedFrequency == 0)
            _accelerateSpeedFrequency = 1f;

        InvokeRepeating(nameof(AccelerateSpeed), _startAccelerateSpeed, _accelerateSpeedFrequency);

        _targetPosition = _planetSpawner.GetTargetPosition();
    }

    private void Update()
    {
        MoveForward();
    }

    static public void ChangeAccelerateSpeedFrequency(float frequency)
    {
        _accelerateSpeedFrequency = frequency;
    }

    public void UpBoost()
    {
        GetBoost?.Invoke();

        _targetPosition = _planetSpawner.GetTargetPosition();
    }

    private void MoveForward()
    {
        _deadLine = new Vector3(_transform.position.x, Vector3.Distance(_transform.position, _deadLine), _targetPosition.z);

        _transform.position = Vector3.MoveTowards(_transform.position, _deadLine, _forwardSpeed * Time.deltaTime);
        //_transform.Translate(Vector3.forward * _forwardSpeed * Time.deltaTime);

        _transform.LookAt(_deadLine);

        if (_transform.position.y <= _deadHight)
            LoseBoost?.Invoke();
    }

    private void AccelerateSpeed()
    {
        _forwardSpeed *= _speedBooster;
        _strafeSpeed *= _speedBooster;

        _mainParticleSystem.simulationSpeed *= _speedBooster;
    }
}
