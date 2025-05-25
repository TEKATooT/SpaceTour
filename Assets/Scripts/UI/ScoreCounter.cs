namespace UI
{
    using Player;
    using TMPro;
    using UnityEngine;

    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private PlayerEngine _player;
        [SerializeField] private TextMeshProUGUI _textPlayerScore;
        [SerializeField] private ScoreScaler _scoreScaler;

        private int _playerScore = 0;

        public int PlayerScore => _playerScore;

        private void OnEnable()
        {
            _player.GetBoosted += OnAddPoint;
        }

        private void OnDisable()
        {
            _player.GetBoosted -= OnAddPoint;
        }

        private void OnAddPoint()
        {
            _playerScore++;

            _scoreScaler.Boosted();

            _textPlayerScore.text = _playerScore.ToString();
        }
    }
}