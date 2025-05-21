using UnityEngine;
using YG;
using Player;

namespace Scripts
{
    public class GameHandler : MonoBehaviour
    {
        private readonly float _normalTime = 1f;
        private readonly float _stopTime = 0f;
        private readonly int _timeBeforeStoppingGame = 1;

        [SerializeField] private PlayerEngine _player;
        [SerializeField] private GameObject _gameOverPanel;

        private bool _isLoseGame = false;
        private bool _isAdOpen = false;

        private void OnEnable()
        {
            YandexGame.onVisibilityWindowGame += OnVisibilityWindowGame;
            YandexGame.OpenFullAdEvent += OnAdOpen;
            YandexGame.CloseFullAdEvent += OnAdClose;

            _player.LoseBoosted += LoseGame;
        }

        private void OnDisable()
        {
            YandexGame.onVisibilityWindowGame -= OnVisibilityWindowGame;
            YandexGame.OpenFullAdEvent -= OnAdOpen;
            YandexGame.CloseFullAdEvent -= OnAdClose;

            _player.LoseBoosted -= LoseGame;

            ResumeGame();
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

        private void OnVisibilityWindowGame(bool isVisible)
        {
            if (isVisible && !_isAdOpen && !_isLoseGame)
                ResumeGame();
            else if (isVisible && !_isAdOpen)
                AudioListener.pause = false;
            else
                StopGame();
        }

        private void OnAdOpen()
        {
            _isAdOpen = true;

            StopGame();
        }
        
        private void OnAdClose()
        {
            _isAdOpen = false;

            ResumeGame();
        }
    }
}