using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Scrollbar _volume;
    [SerializeField] private Toggle _mute;

    static public bool Mute;
    static public float Volume;
    static public string CorrectLanguage;

    private const string EnglishCode = "English";
    private const string RussionCode = "Russian";
    private const string TurkishCode = "Turkish";

    private const string English = "en";
    private const string Russion = "ru";
    private const string Turkish = "tr";

    private const int First = 0;
    private const int Second = 1;
    private const int Third = 2;

    private float _fasterDifference = 0.5f;
    private float _normalDifference = 1;
    private float _slowlyDifference = 1.5f;

    private readonly int _defaultDifference = 0;

    private void Start()
    {
        SelectDifferenceDropBar(_defaultDifference);

        if (YandexGame.EnvironmentData.language == Turkish)
        {
            SelectLanguageDropBar(Third);
            CorrectLanguage = Turkish;
        }
        else if (YandexGame.EnvironmentData.language == Russion)
        {
            SelectLanguageDropBar(Second);
            CorrectLanguage = Russion;
        }
        else
        {
            SelectLanguageDropBar(First);
            CorrectLanguage = English;
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
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(EnglishCode);
                CorrectLanguage = English;
                break;
            case Second:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(RussionCode);
                CorrectLanguage = Russion;
                break;
            case Third:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(TurkishCode);
                CorrectLanguage = Turkish;
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
                PlayerEngine.ChangeAccelerateSpeedFrequency(_normalDifference);
                break;
            case Second:
                PlayerEngine.ChangeAccelerateSpeedFrequency(_slowlyDifference);
                break;
            case Third:
                PlayerEngine.ChangeAccelerateSpeedFrequency(_fasterDifference);
                break;
            default:
                break;
        }
    }

    public void ShowScoreBoard()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
