using UnityEngine;

namespace Planets
{
    public class ModelsPlanets : MonoBehaviour
    {
        private Material[] _defaultsMaterials;
        private Material[] _materialsToChange;

        private void Awake()
        {
            _defaultsMaterials = GetComponent<Renderer>().materials;
        }

        private void OnEnable()
        {
           
        }

        private void Start()
        {

        }

        private void Update()
        {
            Appear();
        }

        private void Appear()
        {
            foreach (var material in _defaultsMaterials)
            {
                
            }

            //_renderer.material.Lerp(_renderer.material, _endMaterial, 1 * Time.deltaTime);
        }
    }
}