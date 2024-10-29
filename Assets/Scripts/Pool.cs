using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private Planet _planet;
    private ObjectPool<Planet> _planetsPool;

    private int _minPoolSize = 50;
    private int _maxPoolSize = 51;

    private readonly float _minCubeLifeTime = 2;
    private readonly float _maxCubeLifeTime = 5;

    public event Action<Transform> TurnedOff;
    public event Action PlanetCreated;

    private void Awake()
    {
        _planetsPool = CreatePool(_planet);
    }

    public ObjectPool<Planet> CreatePool(Planet planet)
    {
        return new ObjectPool<Planet>(() =>
        {
            return Instantiate(planet);
        }, pollObject =>
        {
            pollObject.gameObject.SetActive(true);
        }, pollObject =>
        {
            pollObject.gameObject.SetActive(false);
        }, pollObject =>
        {
            Destroy(pollObject.gameObject);
        }, false, _minPoolSize, _maxPoolSize);
    }

    public Planet GetPlanet()
    {
        Planet newPlanet = _planetsPool.Get();

        PlanetCreated.Invoke();

        StartCoroutine(Release(newPlanet));

        return newPlanet;
    }

    public IEnumerator Release(Planet newPlanet)
    {
        float randomlifeTime = UnityEngine.Random.Range(_minCubeLifeTime, _maxCubeLifeTime);

        yield return new WaitForSeconds(randomlifeTime);

        _planetsPool.Release(newPlanet);
    }
}
