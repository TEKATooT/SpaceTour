using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Scripts;
using Newtonsoft.Json;

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
            CheckAuthorization();
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= YandexGame.LoadCloud;
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

        public void AuthorizationDialogButton()
        {
            if (!YandexGame.auth)
            {
                YandexGame.AuthDialog();

                YandexGame.GetDataEvent += YandexGame.LoadCloud;
            }
            else
            {
                CheckAuthorization();

                _authDialog.SetActive(false);
            }
        }

        private void CheckAuthorization()
        {
            if (YandexGame.auth && CheckBestResult())
            {
                _newRecord.SetActive(true);

                AddNewRecord();
            }
            else if (!YandexGame.auth && _scoreCounter.PlayerScore >= One)
            {
                _authDialog.SetActive(true);
            }
        }

        private bool CheckBestResult()
        {
            YandexGame.savesData.ScoreSave.TryGetValue(MainMenu.CorrectDifference.ToString(), out int score);

            return _scoreCounter.PlayerScore > score;
        }

        private void AddNewRecord()
        {
            YandexGame.NewLeaderboardScores(MainMenu.CorrectDifference.ToString(), _scoreCounter.PlayerScore);

            YandexGame.savesData.ScoreSave[MainMenu.CorrectDifference.ToString()] = _scoreCounter.PlayerScore;
            YandexGame.SaveProgress();
        }
    }
}
