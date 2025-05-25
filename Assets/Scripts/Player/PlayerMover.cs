namespace Player
{
    using UnityEngine;
    using YG;

    [RequireComponent(typeof(PlayerEngine))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMover : MonoBehaviour
    {
        private readonly float _timeLimitAnimation = 2;

        private PlayerEngine _engine;
        private PlayerInput _input;
        private Transform _transform;
        private Animator _animator;

        private Vector3 _forwardMove = new Vector2(0, 0);
        private Vector3 _leftTilt = new Vector3(0, 0, -35);
        private Vector3 _rightTilt = new Vector3(0, 0, 35);
        private Vector3 _noTilt = new Vector3(0, 0, 0);

        private Vector2 _strafeDirection;
        private Vector3 _tiltRotation;

        private void Awake()
        {
            _engine = GetComponent<PlayerEngine>();

            _input = new PlayerInput();

            _transform = transform;

            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _input.Enable();

            Invoke(nameof(AnimatiorOff), _timeLimitAnimation);
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            OnStrafeMove();
        }

        private void AnimatiorOff()
        {
            _animator.enabled = false;
        }

        private void AcceptTilt()
        {
            if (_strafeDirection.x > 0f)
                _tiltRotation = _leftTilt;
            else if (_strafeDirection.x < 0f)
                _tiltRotation = _rightTilt;
            else if (_strafeDirection.y == 0f)
                _tiltRotation = _noTilt;

            _transform.Rotate(_tiltRotation);
        }

        private void OnStrafeMove()
        {
            if (YandexGame.EnvironmentData.isDesktop)
                _strafeDirection = _input.Player.Move.ReadValue<Vector2>();

            _transform.Translate((_strafeDirection * _engine.StrafeSpeed) * Time.deltaTime);

            AcceptTilt();
        }

        public void OnStrafeMoveMobileButton(int vector)
        {
            _strafeDirection = new Vector2(vector, 0);
        }

        public void OnStrafeStopMobileButton()
        {
            _strafeDirection = _forwardMove;
        }
    }
}