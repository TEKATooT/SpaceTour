using UnityEngine;

namespace Planets
{
    public class ModelPlanet : MonoBehaviour
    {
        private Material[] _defaultsMaterials;
        private float _speedToVisible = 1;

        private readonly float _visible = 1.1f;
        private readonly float _invisible = 0;
        private readonly float _firstSecond = 1f;

        private void Awake()
        {
            _defaultsMaterials = GetComponent<Renderer>().materials;
        }

        private void Start()
        {
            if (Time.timeSinceLevelLoad >= _firstSecond)
                ApplyInvisibleStatus();
        }

        private void Update()
        {
            Appear();
        }

        public void ApplyInvisibleStatus()
        {
            foreach (var material in _defaultsMaterials)
            {
                Color newColor = material.color;

                newColor.a = _invisible;

                material.color = newColor;
            }
        }

        private void Appear()
        {
            foreach (var material in _defaultsMaterials)
            {
                Color newColor = material.color;
                float _alfa = material.color.a;

                float newAlfa = Mathf.Lerp(_alfa, _visible, _speedToVisible * Time.deltaTime);

                newColor.a = newAlfa;

                material.color = newColor;
            }
        }
    }
}