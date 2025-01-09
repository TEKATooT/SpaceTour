using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;

    private static int _gameCycle;

    private const int One = 1;

    private readonly float _normalTime = 1f;
    private readonly float _stopTime = 0f;

    private int _frequencyAdShow = 3;

    private void OnEnable()
    {
        YandexGame.onVisibilityWindowGame += OnVisibilityWindowGame;
    }

    private void OnDisable()
    {
        YandexGame.onVisibilityWindowGame -= OnVisibilityWindowGame;
    }

    private void OnVisibilityWindowGame(bool isVisible)
    {
        if (isVisible)
            ResumeGame();
        else
            StopGame();
    }

    public void RestartGameButton()
    {
        _gameCycle++;

        ShowFullSreenAd();

        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenuButton()
    {
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - One);
    }

    public void AddScoreButton()
    {
        if (YandexGame.auth)
        {
            YandexGame.NewLeaderboardScores(MainMenu.CorrectDifference.ToString(), _scoreCounter.PlayerScore);

            //Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - One);
        }
        else
        {
            YandexGame.AuthDialog();
        }
    }

    private void StopGame()
    {
        AudioListener.pause = true;
        Time.timeScale = _stopTime;
    }

    private void ResumeGame()
    {
        AudioListener.pause = false;
        Time.timeScale = _normalTime;
    }

    private void ShowFullSreenAd()
    {
        if (_gameCycle % _frequencyAdShow == 0)
            YandexGame.FullscreenShow();
    }
}
