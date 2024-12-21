using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

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

    private const string EnglishCode = "English";
    private const string RussionCode = "Russian";
    private const string TurkishCode = "Turkish";

    private const string English = "en";
    private const string Russion = "ru";
    private const string Turkish = "tr";

    private int _playerScore = 0;
    private int _frequencyAdShow = 3;

    private readonly float _normalTimeScale = 1f;
    private readonly float _stopTimeScale = 0f;

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

        Debug.Log(YandexGame.isGamePlaying);

        VolumeControl();
        SelectLanguage();

        _gameCycle++;
    }

    private void OnDisable()
    {
        _player.LoseBoost -= LoseGame;
        _player.GetBoost -= AddPoint;

        _value = _volume.value;
        _isMute = _mute.isOn;
    }

    public void StartGame()
    {
        Time.timeScale = _normalTimeScale;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore()
    {
        if (YandexGame.auth)
        {
            YandexGame.NewLeaderboardScores("MidleScoree", _playerScore);
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

        //if (!ShowFullSreenAd())
        //{
        //    Time.timeScale = _stopTimeScale;
        //}
    }

    private void AddPoint()
    {
        _playerScore++;

        _scoreScaler.Boosted();

        _text.text = _playerScore.ToString();
    }

    private void SelectLanguage()
    {
        if (MainMenu.CorrectLanguage == Russion)
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll(RussionCode);
        }
        else if (MainMenu.CorrectLanguage == Turkish)
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll(TurkishCode);
        }
        else
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll(EnglishCode);
        }
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

    private bool ShowFullSreenAd()
    {
        bool isShow = false;

        if (_gameCycle % _frequencyAdShow == 0)
        {
            isShow = true;

            YandexGame.FullscreenShow();
        }

        return isShow;
    }
}
