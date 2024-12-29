using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerEngine _player;

    [SerializeField] private Scrollbar _volume;
    [SerializeField] private Toggle _mute;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _buttonsForMobile;

    [SerializeField] private ScoreScaler _scoreScaler;

    private static int _gameCycle;
    private static float _value;
    private static bool _isMute;

    private int _playerScore = 0;
    private int _frequencyAdShow = 3;

    private readonly float _normalTimeScale = 1f;
    private readonly float _stopTimeScale = 0f;

    private float _midleDifference = 1;
    private float _lowDifference = 1.5f;
    private float _hightDifference = 0.5f;
    private float _correntDifference;

    private void Awake()
    {
        if (!YandexGame.EnvironmentData.isDesktop)
            _buttonsForMobile.SetActive(true);
    }

    private void OnEnable()
    {
        _player.LoseBoost += LoseGame;
        _player.GetBoost += AddPoint;
    }

    private void Start()
    {
        YandexGame.GameplayStart();

        VolumeControl();
        SelectLanguage();
        SelectGameDifficulty();

        _gameCycle++;
    }

    private void OnDisable()
    {
        _player.LoseBoost -= LoseGame;
        _player.GetBoost -= AddPoint;

        _value = _volume.value;
        _isMute = _mute.isOn;
    }

    public void RestartGameButton()
    {
        Time.timeScale = _normalTimeScale;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore()
    {
        if (YandexGame.auth)
        {
            YandexGame.NewLeaderboardScores(MainMenu.CorrectDifference.ToString(), _playerScore);
            //_leaderboardYG.NewScore(_playerScore);

            Time.timeScale = _normalTimeScale;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else
        {
            YandexGame.AuthDialog();
        }
    }

    private void LoseGame()
    {
        _player.gameObject.SetActive(false);
        _gameOverPanel.SetActive(true);

        ShowFullSreenAd();

        Time.timeScale = _stopTimeScale;
    }

    private void AddPoint()
    {
        _playerScore++;

        _scoreScaler.Boosted();

        _text.text = _playerScore.ToString();
    }

    private void SelectLanguage()
    {
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(MainMenu.CorrectLanguage.ToString());
    }

    private void SelectGameDifficulty()
    {
        if (MainMenu.CorrectDifference == MainMenu.Difference.High)
            _correntDifference = _hightDifference;
        else if (MainMenu.CorrectDifference == MainMenu.Difference.Low)
            _correntDifference = _lowDifference;
        else
            _correntDifference = _midleDifference;

        _player.ChangeAccelerateSpeedFrequency(_correntDifference);
    }

    private void VolumeControl()
    {
        if (_gameCycle == 0)
        {
            _volume.value = MainMenu.Volume;
            _mute.isOn = MainMenu.Mute;
        }
        else
        {
            _volume.value = _value;
            _mute.isOn = _isMute;
        }
    }

    private void ShowFullSreenAd()
    {
        if (_gameCycle % _frequencyAdShow == 0)
        {
            YandexGame.FullscreenShow();
        }
    }
}
