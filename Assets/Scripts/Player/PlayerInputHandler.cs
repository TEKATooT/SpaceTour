namespace Player
{
    using UnityEngine;
    using YG;

    [RequireComponent(typeof(PlayerEngine))]
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput _input;
        private PlayerEngine _engine;

        private Vector3 _forwardMove = new Vector2(0, 0);
        private Vector2 _strafeDirection;

        private void Awake()
        {
            _engine = GetComponent<PlayerEngine>();

            _input = new PlayerInput();
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
        }

        public void OnStrafeMoveMobileButton(int vectorX)
        {
            _strafeDirection = new Vector2(vectorX, 0);
        }

        public void OnStrafeStopMobileButton(int vectorX)
        {
            if (Mathf.Approximately(_strafeDirection.x, vectorX))
                _strafeDirection = _forwardMove;
        }

        private void OnStrafeMove()
        {
            if (YandexGame.EnvironmentData.isDesktop)
                _strafeDirection = _input.Player.Move.ReadValue<Vector2>();

            _engine.StrafeMove(_strafeDirection);
        }
    }
}