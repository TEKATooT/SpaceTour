using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerEngine _player;
    [SerializeField] private TextMeshProUGUI _text;

    private float _playerScore = -1;

    private void OnEnable()
    {
        _player.GetBoost += AddPoint;
    }

    private void OnDisable()
    {
        _player.GetBoost -= AddPoint;
    }

    private void AddPoint()
    {
        _playerScore++;

        _text.text = _playerScore.ToString();
    }
}
