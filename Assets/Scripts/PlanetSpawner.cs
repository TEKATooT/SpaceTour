using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pool))]
public class PlanetSpawner : MonoBehaviour
{
    private Pool _pool;
    private Planet _planetToTarget;

    private Queue<Planet> _planetInQueue = new Queue<Planet>();

    private Vector3 _nextPosition;
    private Vector3 _firstPosition = new Vector3(0, 0, 5);

    private float _offsetZPosition = 7.5f;

    private readonly int _oneSpawnQuantuty = 1;
    private readonly int _startSpawnQuantuty = 10;

    private readonly float _minOffsetX = -10f;
    private readonly float _maxOffsetX = 10f;

    private void Awake()
    {
        _pool = GetComponent<Pool>();

        _nextPosition = _firstPosition;

        GenerateNextPlanet(_startSpawnQuantuty);
    }

    public Planet GetTarget()
    {
        _planetToTarget = _planetInQueue.Dequeue();

        _planetToTarget.Destroyed += CyclePlanet;

        return _planetToTarget;
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
        float offsetX = Random.Range(_minOffsetX, _maxOffsetX);

        _nextPosition = new Vector3(_nextPosition.x + offsetX, _nextPosition.y, _nextPosition.z + _offsetZPosition);

        return _nextPosition;
    }

    private void CyclePlanet()
    {
        _planetToTarget.Destroyed -= CyclePlanet;
        _pool.Release(_planetToTarget);

        GenerateNextPlanet(_oneSpawnQuantuty);
    }
}
