using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Scripts;

namespace UI
{
    public class LossMenu : MonoBehaviour
    {
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private AdvertisementsViewer _advertisementsViewer;
        [SerializeField] private GameObject _newRecord;

        private const int One = 1;

        private void OnEnable()
        {
            if (CheckBestResult())
                _newRecord.SetActive(true);
        }

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

                YandexGame.savesData.ScoreSave[MainMenu.CorrectDifference.ToString()] = _scoreCounter.PlayerScore;
                YandexGame.SaveProgress();

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - One);
            }
            else
            {
                YandexGame.AuthDialog();
            }
        }

        private bool CheckBestResult()
        {
            YandexGame.savesData.ScoreSave.TryGetValue(MainMenu.CorrectDifference.ToString(), out int score);

            if (_scoreCounter.PlayerScore > score)
                return true;
            else
                return false;
        }
    }
}
