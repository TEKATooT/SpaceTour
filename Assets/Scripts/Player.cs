using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerEngine))]
public class Player : MonoBehaviour
{
    private PlayerEngine _engine;
    private PlayerInput _input;
    private Transform _transform;

    private Vector2 _strafeDirection;

    private Vector3 _leftTilt = new Vector3(0, 0, -35);
    private Vector3 _rightTilt = new Vector3(0, 0, 35);
    private Vector3 _noTilt = new Vector3(0, 0, 0);

    private void Awake()
    {
        _engine = GetComponent<PlayerEngine>();

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

    private void Update()
    {
        OnStrafeMove();

        if (_transform.position.y <= 0.1f)
        {
            Time.timeScale = 0;

            Debug.Log("Game over");
        }
    }

    private void OnStrafeMove()
    {
        _strafeDirection = _input.Player.Move.ReadValue<Vector2>();
        _transform.Translate(_strafeDirection * _engine.StrafeSpeed * Time.deltaTime);

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
}
