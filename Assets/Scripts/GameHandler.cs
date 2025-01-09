using UnityEngine;
using YG;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private PlayerEngine _player;
    [SerializeField] private GameObject _gameOverPanel;

    private readonly float _normalTime = 1f;
    private readonly float _stopTime = 0f;

    private void OnEnable()
    {
        YandexGame.onVisibilityWindowGame += OnVisibilityWindowGame;

        _player.LoseBoost += LoseGame;
    }

    private void OnDisable()
    {
        YandexGame.onVisibilityWindowGame -= OnVisibilityWindowGame;

        _player.LoseBoost -= LoseGame;
    }

    private void OnVisibilityWindowGame(bool isVisible)
    {
        if (isVisible)
            ResumeGame();
        else
            StopGame();
    }

    private void ResumeGame()
    {
        AudioListener.pause = false;
        Time.timeScale = _normalTime;
    }

    private void StopGame()
    {
        AudioListener.pause = true;
        Time.timeScale = _stopTime;
    }

    private void LoseGame()
    {
        _player.gameObject.SetActive(false);
        _gameOverPanel.SetActive(true);
    }
}
