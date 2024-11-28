using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class StartMenu : MonoBehaviour
{
    static public string correctLanguage;

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

    //public void OnCallGameReadyButtonClick()
    //{
    //    YandexGame.GameReadyAPI();
    //}

    private void Start()
    {
        SelectDifferenceDropBar(_defaultDifference);

        if (YandexGame.EnvironmentData.language == Turkish)
        {
            SelectLanguageDropBar(Third);
            correctLanguage = Turkish;
        }
        else if (YandexGame.EnvironmentData.language == Russion)
        {
            SelectLanguageDropBar(Second);
            correctLanguage = Russion;
        }
        else
        {
            SelectLanguageDropBar(First);
            correctLanguage = English;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + Second);
    }

    public void SelectLanguageDropBar(int index)
    {
        switch (index)
        {
            case First:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(EnglishCode);
                correctLanguage = English;
                break;
            case Second:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(RussionCode);
                correctLanguage = Russion;
                break;
            case Third:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(TurkishCode);
                correctLanguage = Turkish;
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
        Debug.Log("SHOW");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
