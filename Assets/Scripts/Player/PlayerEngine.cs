using Planets;
using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerEngine : MonoBehaviour
    {
        private readonly float _startAccelerateSpeed = 1f;
        private readonly float _defaultDifference = 1;
        private readonly float _deadHight = 0.45f;
        private readonly float _timeLimitAnimation = 2;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private ParticleSystem _destroyPlanetEffect;
        [SerializeField] private ParticleSystem _destroyEffect;
        [SerializeField] private PlanetsSpawner _planetSpawner;

        [SerializeField] private float _forwardSpeed = 3f;
        [SerializeField] private float _strafeSpeed = 5f;
        [SerializeField] private float _speedBooster = 1.05f;

        private ParticleSystem.MainModule _mainParticleSystem;
        private Transform _transform;
        private Animator _animator;

        private Vector3 _targetPosition;
        private Vector3 _deadLine;
        private Vector3 _tiltRotation;
        private Vector3 _leftTilt = new Vector3(0, 0, -35);
        private Vector3 _rightTilt = new Vector3(0, 0, 35);
        private Vector3 _noTilt = new Vector3(0, 0, 0);

        private float _accelerateSpeedFrequency;

        public event Action GetBoosted;
        public event Action LoseBoosted;

        public float StrafeSpeed => _strafeSpeed;

        private void Awake()
        {
            _transform = transform;

            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _mainParticleSystem = _particleSystem.main;

            if (_accelerateSpeedFrequency == 0)
                _accelerateSpeedFrequency = _defaultDifference;

            InvokeRepeating(nameof(AccelerateSpeed), _startAccelerateSpeed, _accelerateSpeedFrequency);
            Invoke(nameof(AnimatiorOff), _timeLimitAnimation);

            _targetPosition = _planetSpawner.GetTargetPosition();
        }

        private void Update()
        {
            MoveForward();
        }

        public void UpBoost()
        {
            GetBoosted?.Invoke();

            _audioSource.Play();
            _destroyPlanetEffect.Play();

            _targetPosition = _planetSpawner.GetTargetPosition();
        }

        public void StrafeMove(Vector2 strafeDirection)
        {
            _transform.Translate((strafeDirection * StrafeSpeed) * Time.deltaTime);

            AcceptTilt(strafeDirection);
        }

        public void ChangeAccelerateSpeedFrequency(float frequency)
        {
            _accelerateSpeedFrequency = frequency;
        }

        private void AnimatiorOff()
        {
            _animator.enabled = false;
        }

        private void MoveForward()
        {
            _deadLine = new Vector3(_transform.position.x, Vector3.Distance(_transform.position, _deadLine), _targetPosition.z);

            _transform.position = Vector3.MoveTowards(_transform.position, _deadLine, _forwardSpeed * Time.deltaTime);

            _transform.LookAt(_deadLine);

            if (_transform.position.y <= _deadHight)
            {
                _destroyEffect.gameObject.SetActive(true);
                _destroyEffect.transform.parent = null;

                LoseBoosted?.Invoke();
            }
        }

        private void AcceptTilt(Vector2 strafeDirection)
        {
            if (strafeDirection.x > 0f)
                _tiltRotation = _leftTilt;
            else if (strafeDirection.x < 0f)
                _tiltRotation = _rightTilt;
            else if (strafeDirection.y == 0f)
                _tiltRotation = _noTilt;

            _transform.Rotate(_tiltRotation);
        }

        private void AccelerateSpeed()
        {
            _forwardSpeed *= _speedBooster;
            _strafeSpeed *= _speedBooster;

            _mainParticleSystem.simulationSpeed *= _speedBooster;
        }
    }
}