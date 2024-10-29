using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private Pool _pool;
    
    private Transform[] _spawenPoints;
    private WaitForSeconds _second = new WaitForSeconds(1);

    private void OnEnable()
    {
        _pool.TurnedOff += Generate;
    }

    private void OnDisable()
    {
        _pool.TurnedOff -= Generate;
    }

    private Vector3 GetPosition()
    {
        int randomPosition = Random.Range(0, _spawenPoints.Length);

        return _spawenPoints[randomPosition].position;
    }

    private void Generate(Transform transform)
    {
        Planet planet = _pool.GetPlanet();

        planet.transform.position = transform.position;
    }
}
