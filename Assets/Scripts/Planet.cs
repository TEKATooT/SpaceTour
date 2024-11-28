using System;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private PrefabPlanet[] _planets;

    public event Action Destroyed;

    private void Start()
    {
        int randomType = UnityEngine.Random.Range(0, _planets.Count());

        _planets[randomType].gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroyed?.Invoke();

        if (other.TryGetComponent(out PlayerEngine player))
        {
            player.UpBoost();
        }
    }
}
