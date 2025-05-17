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
        [SerializeField] private GameObject _addRecord;

        private const int One = 1;

        private void OnEnable()
        {
            CheckAuthorization();
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= OnAuthorization;
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
            YandexGame.AuthDialog();

            YandexGame.GetDataEvent += OnAuthorization;
        }

        public void AddRecordButton()
        {
            CheckAuthorization();

            _addRecord.SetActive(false);
        }

        private void OnAuthorization()
        {
            YandexGame.LoadCloud();

            _authDialog.SetActive(false);

            _addRecord.SetActive(true);
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
