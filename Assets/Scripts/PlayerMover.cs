using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PlanetSpawner _planetSpawner;

    [SerializeField] private float _forwardSpeed = 3f;
    [SerializeField] private float _strafeSpeed = 5f;
    [SerializeField] private float _speedBooster = 1.05f;

    private PlayerInput _input;
    private Transform _transform;
    private Planet _targetPlanet;

    private Vector2 _strafeDirection;
    private Vector3 _deadLine;
    private Vector3 _upLine;

    private Vector3 _leftTilt = new Vector3(0, 0, -35);
    private Vector3 _rightTilt = new Vector3(0, 0, 35);
    private Vector3 _noTilt = new Vector3(0, 0, 0);

    private bool _isBoosted;

    private float _zPositionBetweenPlanets;
    private float _highestYPosition = 5f;
    private float _accelerateSpeedFrequency = 1f;

    private readonly float _startAccelerateSpeed = 1f;

    public event Action GetBoost;

    private void Awake()
    {
        _input = new PlayerInput();

        //_input.Player.Move.performed += OnMove;

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

        InvokeRepeating(nameof(AccelerateSpeed), _startAccelerateSpeed, _accelerateSpeedFrequency);
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
    }

    public void UpBoost()
    {
        GetBoost?.Invoke();

        _isBoosted = true;

        _targetPlanet = _planetSpawner.GetTarget();

        _zPositionBetweenPlanets = Mathf.Lerp(_transform.position.z, _targetPlanet.transform.position.z, 0.5f);
    }

    private void ForwardMove()
    {
        if (_isBoosted)
        {
            _upLine = new Vector3(_transform.position.x, _highestYPosition, _zPositionBetweenPlanets);

            _transform.LookAt(_upLine);

            if (_transform.position.y >= _highestYPosition)
            {
                _isBoosted = false;
            }
        }
        else
        {
            _deadLine = new Vector3(_transform.position.x, _targetPlanet.transform.position.y, _targetPlanet.transform.position.z);

            _transform.LookAt(_deadLine);
        }

        _transform.Translate(Vector3.forward * _forwardSpeed * Time.deltaTime);

        //_transform.position = Vector3.MoveTowards(_transform.position, _deadLine, _forwardSpeed * Time.deltaTime);
    }

    private void OnStrafeMove()
    {
        _strafeDirection = _input.Player.Move.ReadValue<Vector2>();

        _transform.Translate(_strafeDirection * _strafeSpeed * Time.deltaTime);

        AcceptTilt();
    }

    private void AcceptTilt()
    {
        if (_strafeDirection.x > 0)
            _transform.Rotate(_leftTilt);

        else if (_strafeDirection.x < 0)
            _transform.Rotate(_rightTilt);

        else if (_strafeDirection.x == 0)
            _transform.Rotate(_noTilt);
    }

    //private void OnMove(InputAction.CallbackContext context)
    //{
    //    _strafeDirection = context.action.ReadValue<Vector2>();
    //}

    private void AccelerateSpeed()
    {
        _forwardSpeed *= _speedBooster;
        _strafeSpeed *= _speedBooster;
    }
}
