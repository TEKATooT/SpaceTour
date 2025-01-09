using TMPro;
using UnityEngine;
using YG;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private PlayerEngine _player;

    [SerializeField] private TextMeshProUGUI _textPlayerScore;
    [SerializeField] private GameObject _gameOverPanel;

    [SerializeField] private ScoreScaler _scoreScaler;

    private int _playerScore = 0;
    public int PlayerScore => _playerScore;

    private void OnEnable()
    {
        _player.LoseBoost += LoseGame;
        _player.GetBoost += AddPoint;
    }

    private void OnDisable()
    {
        _player.LoseBoost -= LoseGame;
        _player.GetBoost -= AddPoint;
    }

    private void LoseGame()
    {
        _player.gameObject.SetActive(false);
        _gameOverPanel.SetActive(true);
    }

    private void AddPoint()
    {
        _playerScore++;

        _scoreScaler.Boosted();

        _textPlayerScore.text = _playerScore.ToString();
    }
}
