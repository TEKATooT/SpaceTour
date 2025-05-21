using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Planets
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] private Planet _planet;

        private ObjectPool<Planet> _planetsPool;

        private int _minSize = 50;
        private int _maxSize = 51;

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
            }, false, _minSize, _maxSize);
        }

        public Planet GetPlanet()
        {
            Planet newPlanet = _planetsPool.Get();

            PlanetCreated?.Invoke();

            return newPlanet;
        }

        public void Release(Planet planet)
        {
            _planetsPool.Release(planet);
        }
    }
}