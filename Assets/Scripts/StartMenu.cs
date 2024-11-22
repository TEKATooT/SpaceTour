using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class StartMenu : MonoBehaviour
{
    private float _fasterDifference = 0.5f;
    private float _normalDifference = 1;
    private float _slowlyDifference = 1.5f;

    private readonly int _defaultDifference = 2;

    private void OnEnable()
    {
        SelectDifferenceDropBar(_defaultDifference);
    }

    private void Start()
    {
        if (YandexGame.EnvironmentData.language == "tr")
        {
            SelectLanguageDropBar(2);
        }
        else if (YandexGame.EnvironmentData.language == "ru")
        {
            SelectLanguageDropBar(1);
        }
        else
        {
            SelectLanguageDropBar(0);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SelectDifferenceDropBar(int index)
    {
        switch (index)
        {
            case 0:
                PlayerEngine.ChangeAccelerateSpeedFrequency(_fasterDifference);
                break;
            case 1:
                PlayerEngine.ChangeAccelerateSpeedFrequency(_normalDifference);
                break;
            case 2:
                PlayerEngine.ChangeAccelerateSpeedFrequency(_slowlyDifference);
                break;
            default:
                break;
        }
    }

    public void SelectLanguageDropBar(int index)
    {
        switch (index)
        {
            case 0:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
                break;
            case 1:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Russian");
                break;
            case 2:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Turkish");
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
