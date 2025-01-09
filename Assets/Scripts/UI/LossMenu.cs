using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LossMenu : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private AdvertisementsViewer _advertisementsViewer;

    private const int One = 1;
    public void RestartGameButton()
    {
        _advertisementsViewer.ShowFullSreenAd();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - One);
    }

    public void AddScoreButton()
    {
        if (YandexGame.auth)
        {
            YandexGame.NewLeaderboardScores(MainMenu.CorrectDifference.ToString(), _scoreCounter.PlayerScore);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - One);
        }
        else
        {
            YandexGame.AuthDialog();
        }
    }
}
