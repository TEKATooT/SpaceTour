using TMPro;
using UnityEngine;
using YG;
using Player;

namespace UI
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private PlayerEngine _player;
        [SerializeField] private TextMeshProUGUI _textPlayerScore;
        [SerializeField] private ScoreScaler _scoreScaler;

        private int _playerScore = 0;
        public int PlayerScore => _playerScore;

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

            _scoreScaler.Boosted();

            _textPlayerScore.text = _playerScore.ToString();
        }
    }
}