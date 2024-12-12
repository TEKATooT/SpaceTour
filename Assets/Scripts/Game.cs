using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class Game : MonoBehaviour
{
    [SerializeField] private Scrollbar _volume;
    [SerializeField] private Toggle _mute;

    [SerializeField] private PlayerEngine _player;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _gameOverPanel;

    [SerializeField] private GameObject _buttonsForMobile;

    [SerializeField] private ScoreScaler _scoreScaler;

    private const string EnglishCode = "English";
    private const string RussionCode = "Russian";
    private const string TurkishCode = "Turkish";

    private const string English = "en";
    private const string Russion = "ru";
    private const string Turkish = "tr";

    private int _playerScore = 0;

    private readonly float _normalTimeScale = 1f;
    private readonly float _stopTimeScale = 0f;

    private void Awake()
    {
        if (!YandexGame.EnvironmentData.isDesktop)
        {
            _buttonsForMobile.SetActive(true);
        }
    }

    private void OnEnable()
    {
        _player.LoseBoost += LoseGame;
        _player.GetBoost += AddPoint;
    }

    private void Start()
    {
        _volume.value = MainMenu.Volume;
        _mute.isOn = MainMenu.Mute;

        SelectLanguage();
    }

    private void OnDisable()
    {
        _player.LoseBoost -= LoseGame;
        _player.GetBoost -= AddPoint;
    }

    public void StartGame()
    {
        Time.timeScale = _normalTimeScale;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore()
    {
        Debug.Log(_playerScore);

        if (YandexGame.auth)
        {
            YandexGame.NewLeaderboardScores("Score", _playerScore);
            //_leaderboardYG.NewScore(_playerScore);
        }
        else
        {
            YandexGame.AuthDialog();
        }
    }

    private void LoseGame()
    {
        Time.timeScale = _stopTimeScale;

        _player.gameObject.SetActive(false);
        _gameOverPanel.SetActive(true);
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
}
