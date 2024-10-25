using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed = 3f;
    [SerializeField] private Planet _targetPlanet;

    private Transform _transform;
    private Vector3 _deadLine;

    private readonly float _startHeight = 5f;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        _targetPlanet.Destroyed += MoveBoost;
    }

    private void OnDisable()
    {
        _targetPlanet.Destroyed -= MoveBoost;
    }

    private void Start()
    {

    }

    private void Update()
    {
        _deadLine = new Vector3(_transform.position.x, _targetPlanet.transform.position.y, _targetPlanet.transform.position.z);

        _transform.position = Vector3.MoveTowards(_transform.position, _deadLine, _forwardSpeed * Time.deltaTime);

        //_transform.LookAt(_deadLine);
    }

    private void MoveBoost()
    {
        Vector3 defaultHeight = _transform.position;

        defaultHeight.y = _startHeight;

        _transform.position = Vector3.Lerp(_transform.position, defaultHeight, 1);
    }

    public void OnLeftStrafe()
    {
        _transform.Translate(Vector3.left * 0.2f);
    }

    public void OnRightStrafe()
    {
        _transform.Translate(Vector3.right * 0.2f);
    }
}
