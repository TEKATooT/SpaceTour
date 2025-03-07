using UnityEngine;
using Player;

namespace Scripts
{
    public class CameraFollows : MonoBehaviour
    {
        [SerializeField] private PlayerMover _player;
        [SerializeField] private float _zOffset = -10;

        private Transform _transform;
        private Vector3 _position;

        private void Start()
        {
            _transform = transform;
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
    }
}