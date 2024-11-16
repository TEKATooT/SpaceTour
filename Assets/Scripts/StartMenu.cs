using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private float _fasterDifference = 0.5f;
    private float _normalDifference = 1;
    private float _slowlyDifference = 1.5f;

    private void OnEnable()
    {
        SelectDifference(_normalDifference);
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
                SelectDifference(_fasterDifference);
                break;
            case 1:
                SelectDifference(_normalDifference);
                break;
            case 2:
                SelectDifference(_slowlyDifference);
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
                SelectLanguage('e');
                break;
            case 1:
                SelectLanguage('r');
                break;
            case 2:
                SelectLanguage('t');
                break;
            default:
                break;
        }
    }

        private void SelectDifference(float frequency)
    {
        PlayerEngine.ChangeAccelerateSpeedFrequency(frequency);
    }

    public void SelectLanguage(char language)
    {

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
