using UnityEngine;
using YG;
using Player;

namespace Scripts
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private PlayerEngine _player;
        [SerializeField] private GameObject _gameOverPanel;

        private int _timeBeforeStoppingGame = 1;
        private bool _isLoseGame = false;

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

            ResumeGame();
        }

        private void OnVisibilityWindowGame(bool isVisible)
        {
            if (isVisible && _isLoseGame == false)
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
            _isLoseGame = true;
            _player.gameObject.SetActive(false);
            _gameOverPanel.SetActive(true);

            Invoke(nameof(StopTime), _timeBeforeStoppingGame);
        }

        private void StopTime()
        {
            Time.timeScale = _stopTime;
        }
    }
}