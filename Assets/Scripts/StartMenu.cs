using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private float _fasterDifference = 0.25f;
    private float _normalDifference = 1f;
    private float _slowlyDifference = 1.5f;

    private void OnEnable()
    {
        SelectDifference(_fasterDifference);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SelectDifference(float frequency)
    {
        PlayerEngine.ChangeAccelerateSpeedFrequency(frequency);

        Debug.Log(frequency);
    }

    public void SelectLanguage()
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
