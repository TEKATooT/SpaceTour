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
        [SerializeField] private GameObject _authDialog;

        private const int One = 1;

        private void OnEnable()
        {
            CheckYandexGameAuthorization();
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
            YandexGame.AuthDialog();

            _authDialog.SetActive(false);

            CheckYandexGameAuthorization();
        }

        private void CheckYandexGameAuthorization()
        {
            if (CheckBestResult() && YandexGame.auth)
            {
                _newRecord.SetActive(true);
                AddNewRecord();
            }
            else if (CheckBestResult() && !YandexGame.auth)
            {
                _authDialog.SetActive(true);
            }
        }

        private bool CheckBestResult()
        {
            YandexGame.savesData.ScoreSave.TryGetValue(MainMenu.CorrectDifference.ToString(), out int score);

            if (_scoreCounter.PlayerScore > score && _scoreCounter.PlayerScore >= One)
                return true;
            else
                return false;
        }

        private void AddNewRecord()
        {
            YandexGame.NewLeaderboardScores(MainMenu.CorrectDifference.ToString(), _scoreCounter.PlayerScore);

            YandexGame.savesData.ScoreSave[MainMenu.CorrectDifference.ToString()] = _scoreCounter.PlayerScore;
            YandexGame.SaveProgress();
        }
    }
}
