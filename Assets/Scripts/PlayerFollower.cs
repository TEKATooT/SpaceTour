using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _offset = -10;

    private Transform _transform;
    private Vector3 _position;

    private void Start()
    {
        _transform = transform;
    }
    private void Update()
    {
        _position = _transform.position;

        _position.z = _player.transform.position.z + _offset;

        _transform.position = _position;
    }
}