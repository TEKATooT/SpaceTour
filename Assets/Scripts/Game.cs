using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerEngine _player;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _gameOverPanel;

    private int _playerScore = 0;

    private readonly float _normalTimeScale = 1f;
    private readonly float _stopTimeScale = 0f;

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

    public void StartGame()
    {
        Time.timeScale = _normalTimeScale;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore()
    {
        Debug.Log(_playerScore);
    }

    private void LoseGame()
    {
        Time.timeScale = _stopTimeScale;

        _player.gameObject.SetActive(false);
        _gameOverPanel.SetActive(true);
    }

    private void AddPoint()
    {
        _playerScore++;

        _text.text = _playerScore.ToString();
    }
}
