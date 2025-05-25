namespace Planets
{
    using Player;
    using System;
    using System.Linq;
    using UnityEngine;

    public class Planet : MonoBehaviour
    {
        private readonly float _minRotationSpeed = -500f;
        private readonly float _maxRotationSpeed = 500f;
        private readonly float _minAngle = -180f;
        private readonly float _maxRAngle = 180f;

        [SerializeField]
        private ModelPlanet[] _planets;

        private ModelPlanet _planet;

        private float _randomAngle;
        private float _randomRotationSpeed;

        private Vector3 _defaultSize;

        private bool _isFirstPlanet = true;

        public event Action Destroyed;

        private void Awake()
        {
            _defaultSize = transform.localScale;
        }

        private void OnEnable()
        {
            MakeRandomPlanetAngles();

            ChooseRandomModel();
        }

        private void OnDisable()
        {
            _planet.gameObject.SetActive(false);

            transform.localScale = _defaultSize;
        }

        private void Update()
        {
            transform.RotateAround(transform.position, Vector3.up, _randomRotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroyed?.Invoke();

            if (other.TryGetComponent(out PlayerEngine player))
            {
                player.UpBoost();
            }
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

            ApplyInvisibleStatus();
            ApplyPlanetSize(randomModel);
        }

        private void ApplyInvisibleStatus()
        {
            if (!_isFirstPlanet)
            {
                _planet.ApplyInvisibleStatus();
            }
            else
            {
                _isFirstPlanet = false;
            }
        }

        private void ApplyPlanetSize(int randomSize)
        {
            int defaultSize = 1;
            float correctRatio = 1.3f;
            float divisor = 10f;

            if (randomSize <= defaultSize)
            {
                ++randomSize;
            }

            transform.localScale *= (correctRatio + randomSize / divisor);
        }
    }
}