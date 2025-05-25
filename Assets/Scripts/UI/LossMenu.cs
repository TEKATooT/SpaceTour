namespace UI
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using YG;

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

        private IEnumerator WaitLoadCloud()
        {
            yield return _delayForLoadCloud;

            if (!CheckBestResult())
                _notNewRecord.SetActive(true);
        }

        private void OnAuthorization()
        {
            YandexGame.LoadCloud();

            _authDialog.SetActive(false);

            StartCoroutine(nameof(WaitLoadCloud));
        }

        public void OnRestartGameButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnLoadMainMenuButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - One);
        }

        public void OnAuthorizationDialogButton()
        {
            YandexGame.AuthDialog();

            YandexGame.GetDataEvent += OnAuthorization;
        }
    }
}
