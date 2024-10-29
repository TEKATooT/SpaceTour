using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Planet _targetPlanet;
    [SerializeField] private float _forwardSpeed = 3f;
    [SerializeField] private float _strafeSpeed = 0.025f;

    private PlayerInput _input;

    private Vector3 _strafeDirection;
    private Transform _transform;
    private Vector3 _deadLine;

    private readonly float _startHeight = 5f;

    private void Awake()
    {
        _input = new PlayerInput();

       // _input.Player.Move.performed += OnMove;

        _transform = transform;
    }

    private void OnEnable()
    {
        _input.Enable();

        _targetPlanet.Destroyed += MoveBoost;
    }

    private void OnDisable()
    {
        _input.Disable();

        _targetPlanet.Destroyed -= MoveBoost;
    }

    private void Update()
    {
        _deadLine = new Vector3(_transform.position.x, _targetPlanet.transform.position.y, _targetPlanet.transform.position.z);

        _transform.position = Vector3.MoveTowards(_transform.position, _deadLine, _forwardSpeed * Time.deltaTime);

        Move();

        //_transform.LookAt(_deadLine);
    }

    private void MoveBoost()
    {
        Vector3 defaultHeight = _transform.position;

        defaultHeight.y = _startHeight;

        _transform.position = Vector3.Lerp(_transform.position, defaultHeight, 1);
    }

    //private void OnMove(InputAction.CallbackContext context)
    //{
    //    _strafeDirection = context.action.ReadValue<Vector2>();
    //}

    private void Move()
    {
        _strafeDirection = _input.Player.Move.ReadValue<Vector2>();

        _transform.Translate(_strafeDirection * _strafeSpeed);
    }
}
