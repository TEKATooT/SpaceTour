using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    [RequireComponent(typeof(Pool))]
    public class PlanetsSpawner : MonoBehaviour
    {
        private readonly int _minPositionX = -5;
        private readonly int _maxPositionX = 5;
        private readonly int _startSpawnQuantuty = 5;
        private readonly int _oneSpawnQuantuty = 1;
        private readonly float _offsetZPosition = 4f;

        [SerializeField] private ParticleSystem _destroyEffect;

        private Pool _pool;
        private Planet _planetToTarget;

        private Queue<Planet> _planetInQueue = new Queue<Planet>();

        private Vector3 _nextPosition;

        private int _lastPositon = 10;

        private void Awake()
        {
            _pool = GetComponent<Pool>();

            GenerateNextPlanet(_startSpawnQuantuty);
        }

        public Vector3 GetTargetPosition()
        {
            _planetToTarget = _planetInQueue.Dequeue();

            _planetToTarget.Destroyed += OnLoop;

            return _planetToTarget.transform.position;
        }

        private void GenerateNextPlanet(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                Planet planet = _pool.GetPlanet();
                planet.transform.position = GetPosition();

                _planetInQueue.Enqueue(planet);
            }
        }

        private Vector3 GetPosition()
        {
            int positionX = Random.Range(_minPositionX, _maxPositionX);

            if (_lastPositon == positionX)
                positionX = RerollPosition(_lastPositon);

            _nextPosition = new Vector3(positionX, _nextPosition.y, _nextPosition.z + _offsetZPosition);

            _lastPositon = positionX;

            return _nextPosition;
        }

        private int RerollPosition(int lastPositon)
        {
            int newPosition = Random.Range(_minPositionX, _maxPositionX);

            while (lastPositon == newPosition)
            {
                newPosition = Random.Range(_minPositionX, _maxPositionX);
            }

            return newPosition;
        }

        private void DestroyEffect()
        {
            _destroyEffect.transform.position = _planetToTarget.transform.position;

            _destroyEffect.Play();
        }

        private void OnLoop()
        {
            DestroyEffect();

            _planetToTarget.Destroyed -= OnLoop;
            _pool.Release(_planetToTarget);

            GenerateNextPlanet(_oneSpawnQuantuty);
        }
    }
}