using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using System.Collections;

namespace UI
{
    public class LossMenu : MonoBehaviour
    {
        private const int One = 1;

        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private GameObject _newRecord;
        [SerializeField] private GameObject _authDialog;
        [SerializeField] private GameObject _notNewRecord;

        private WaitForSecondsRealtime _delayForLoadCloud = new WaitForSecondsRealtime(2f);

        private void OnEnable()
        {
            CheckBestResult();
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= OnAuthorization;
        }

        public void RestartGameButton()
        {
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

        private void OnAuthorization()
        {
            YandexGame.LoadCloud();

            _authDialog.SetActive(false);

            StartCoroutine(nameof(WaitLoadCloud));
        }

        private IEnumerator WaitLoadCloud()
        {
            yield return _delayForLoadCloud;

            if (!CheckBestResult())
                _notNewRecord.SetActive(true);
        }

        private bool CheckBestResult()
        {
            if (YandexGame.auth && CheckCloudRecord())
            {
                _newRecord.SetActive(true);

                AddNewRecord();

                return true;
            }
            else if (!YandexGame.auth && _scoreCounter.PlayerScore >= One)
            {
                _authDialog.SetActive(true);
            }

            return false;
        }

        private bool CheckCloudRecord()
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
