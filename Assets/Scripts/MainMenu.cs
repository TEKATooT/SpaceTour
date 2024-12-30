using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Scrollbar _volume;
    [SerializeField] private Toggle _mute;

    [SerializeField] private LeaderboardYG _leaderboardYG;

    static public bool Mute;
    static public float Volume;
    static public Language CorrectLanguage;
    static public Difference CorrectDifference;

    private const string EnglishYaCode = "en";
    private const string RussianYaCode = "ru";
    private const string TurkishYaCode = "tr";

    private const int First = 0;
    private const int Second = 1;
    private const int Third = 2;

    private readonly int _defaultDifference = 0;

    public enum Language
    {
        English, Russian, Turkish
    }

    public enum Difference
    {
        Low, Midle, High
    }

    private void OnEnable()
    {
        YandexGame.onVisibilityWindowGame += OnVisibilityWindowGame;
    }

    private void Start()
    {
        SelectDifferenceDropBar(_defaultDifference);

        if (YandexGame.EnvironmentData.language == TurkishYaCode)
        {
            SelectLanguageDropBar(Third);               // ?
            CorrectLanguage = Language.Turkish;
        }
        else if (YandexGame.EnvironmentData.language == RussianYaCode)
        {
            SelectLanguageDropBar(Second);
            CorrectLanguage = Language.Russian;
        }
        else
        {
            SelectLanguageDropBar(First);
            CorrectLanguage = Language.English;
        }
    }

    private void OnDisable()
    {
        YandexGame.onVisibilityWindowGame -= OnVisibilityWindowGame;
    }

    private void OnVisibilityWindowGame(bool isVisible)
    {
        if (isVisible)
        {
            AudioListener.pause = false;
            Time.timeScale = 1;
        }
        else
        {
            AudioListener.pause = true;
            Time.timeScale = 0;
        }
    }

    public void StartGame()
    {
        Volume = _volume.value;
        Mute = _mute.isOn;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + Second);
    }

    public void SelectLanguageDropBar(int index)
    {
        switch (index)
        {
            case First:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(Language.English.ToString());
                CorrectLanguage = Language.English;
                break;
            case Second:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(Language.Russian.ToString());
                CorrectLanguage = Language.Russian;
                break;
            case Third:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(Language.Turkish.ToString());
                CorrectLanguage = Language.Turkish;
                break;
            default:
                break;
        }
    }

    public void SelectDifferenceDropBar(int index)
    {
        switch (index)
        {
            case First:
                _leaderboardYG.SetNameLB(Difference.Midle.ToString());
                CorrectDifference = Difference.Midle;
                break;
            case Second:
                _leaderboardYG.SetNameLB(Difference.Low.ToString());
                CorrectDifference = Difference.Low;
                break;
            case Third:
                _leaderboardYG.SetNameLB(Difference.High.ToString());
                CorrectDifference = Difference.High;
                break;
            default:
                break;
        }

        _leaderboardYG.UpdateLB();
    }

    public void ShowScoreBoard()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
