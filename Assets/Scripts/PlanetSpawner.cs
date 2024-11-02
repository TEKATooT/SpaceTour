using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private Pool _pool;

    [SerializeField] private Planet _planet;

    private Transform[] _spawenPoints;
         
    private Vector3 _nextPosition;
    private Vector3 _startPosition = new Vector3(0, 0, 10);

    private WaitForSeconds _second = new WaitForSeconds(1);

    private float _offsetZPosition = 7.5f;

    private void Awake()
    {
        _nextPosition = _startPosition;
    }

    private void OnEnable()
    {
        //_pool.TurnedOff += Generate;
    }

    private void OnDisable()
    {
        //_pool.TurnedOff -= Generate;
    }

    private void Start()
    {
        Generate();
        Generate();
        Generate();
    }

    private Vector3 GetPosition()
    {
        float offsetX = Random.Range(-10f, 10f);

        Debug.Log(offsetX);

        _nextPosition = new Vector3(_nextPosition.x + offsetX, _nextPosition.y, _nextPosition.z + _offsetZPosition);

        return _nextPosition;
    }

    public Planet Generate()
    {
        //Planet planet = _pool.GetPlanet();

        //planet.transform.position = transform.position;

       Planet nextPlanet = Instantiate(_planet, GetPosition(), Quaternion.identity);

        return nextPlanet;
    }
}
