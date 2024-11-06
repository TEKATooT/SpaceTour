using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PlanetSpawner _planetSpawner;

    [SerializeField] private float _forwardSpeed = 3f;
    [SerializeField] private float _strafeSpeed = 5f;
    [SerializeField] private float _speedBooster = 1.01f;

    private Planet _targetPlanet;
    private PlayerInput _input;
    private Transform _transform;
    private Vector2 _strafeDirection;
    private Vector3 _deadLine;

    private bool _isBoosted;

    private Vector3 _newHeightPosition;
    private float _standardHeight = 5f;

    public float AccelerateSpeedFrequency = 1f;
    private readonly float _startAccelerateSpeed = 1f;

    private void Awake()
    {
        _input = new PlayerInput();

        _input.Player.Move.performed += OnMove;

        _transform = transform;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Start()
    {
        UpBoost();

        InvokeRepeating(nameof(AccelerateSpeed), _startAccelerateSpeed, AccelerateSpeedFrequency);
    }

    private void Update()
    {
        ForwardMove();

        OnStrafeMove();

        if (_transform.position.y == 0)
        {
            Time.timeScale = 0;

            Debug.Log("Game over");

            Destroy(this);
        }
        //_transform.LookAt(_deadLine);
    }

    public void UpBoost()
    {
        _newHeightPosition = _transform.position;

        _newHeightPosition.y = _standardHeight;

        _transform.position = Vector3.Lerp(_transform.position, _newHeightPosition, 1);

        _targetPlanet = _planetSpawner.GetTarget();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _strafeDirection = context.action.ReadValue<Vector2>();
    }

    private void ForwardMove()
    {
        _deadLine = new Vector3(_transform.position.x, _targetPlanet.transform.position.y, _targetPlanet.transform.position.z);

        _transform.position = Vector3.MoveTowards(_transform.position, _deadLine, _forwardSpeed * Time.deltaTime);
    }

    private void OnStrafeMove()
    {
        //_strafeDirection = _input.Player.Move.ReadValue<Vector2>();

        _transform.Translate(_strafeDirection * _strafeSpeed * Time.deltaTime);
    }

    private void AccelerateSpeed()
    {
        _forwardSpeed *= _speedBooster;
        _strafeSpeed *= _speedBooster;
    }
}
