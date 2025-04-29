using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Scripts;
using Newtonsoft.Json.Serialization;
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
            Debug.Log($"SAVES {JsonConvert.SerializeObject(YandexGame.savesData, Formatting.Indented)}");

            YandexGame.AuthDialog();

            CheckYandexGameAuthorization();
        }

        private void CheckYandexGameAuthorization()
        {
            if (YandexGame.auth && CheckBestResult())
            {
                _authDialog.SetActive(false);
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
            Debug.Log($"SAVES {JsonConvert.SerializeObject(YandexGame.savesData, Formatting.Indented)}");
            
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
