using UnityEngine;
using Player;
using YG;

namespace Scripts
{
    public class CameraFollows : MonoBehaviour
    {
        private readonly float _defaultView = 90;
        private readonly float _zoomViewForLandScape = 55;

        [SerializeField] private PlayerMover _player;
        [SerializeField] private float _zOffset = -9f;

        private Transform _transform;
        private Vector3 _position;
        private Camera _camera;

        private void Start()
        {
            _transform = transform;

            if (gameObject.TryGetComponent(out Camera camera))
                _camera = camera;

            if (YandexGame.EnvironmentData.isMobile)
                CorrectionVision();
        }

        private void Update()
        {
            Follow();
        }

        private void Follow()
        {
            _position = _transform.position;

            _position.z = _player.transform.position.z + _zOffset;

            _transform.position = _position;
        }

        private void CorrectionVision()
        {
            if (Screen.orientation != ScreenOrientation.Portrait)
                _camera.fieldOfView = _zoomViewForLandScape;
            else
                _camera.fieldOfView = _defaultView;
        }
    }
}