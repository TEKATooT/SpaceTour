using System;
using System.Linq;
using UnityEngine;
using Player;

namespace Planets
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private ModelPlanet[] _planets;

        private ModelPlanet _planet;

        private bool _isStartedPlanet = true;

        private float _minRotationSpeed = -500f;
        private float _maxRotationSpeed = 500f;
        private float _randomRotationSpeed;

        private float _minAngle = -180f;
        private float _maxRAngle = 180f;
        private float _randomAngle;

        public event Action Destroyed;

        private void OnEnable()
        {
            MakeRandomPlanetAngles();

            ChooseRandomModel();
        }

        private void OnDisable()
        {
            _planet.gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.RotateAround(transform.position, Vector3.up, _randomRotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroyed?.Invoke();

            if (other.TryGetComponent(out PlayerEngine player))
                player.UpBoost();
        }

        private void MakeRandomPlanetAngles()
        {
            _randomRotationSpeed = UnityEngine.Random.Range(_minRotationSpeed, _maxRotationSpeed);
            _randomAngle = UnityEngine.Random.Range(_minAngle, _maxRAngle);

            transform.Rotate(_randomRotationSpeed, transform.rotation.y, transform.rotation.z);
        }

        private void ChooseRandomModel()
        {
            int randomModel = UnityEngine.Random.Range(0, _planets.Count());

            _planets[randomModel].gameObject.SetActive(true);

            _planet = _planets[randomModel];

            AppearEffectOff();
        }

        private void AppearEffectOff()
        {
            if (_isStartedPlanet && _planet.TryGetComponent(out ModelPlanet modelScript))
            {
                modelScript.enabled = false;
                _isStartedPlanet = false;
            }
        }
    }
}