using System;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private ModelsPlanets[] _planets;

    public event Action Destroyed;

    private void Start()
    {
        int randomModel = UnityEngine.Random.Range(0, _planets.Count());

        _planets[randomModel].gameObject.SetActive(true);
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
