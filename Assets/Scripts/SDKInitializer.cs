using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SDKInitializer : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialized);
    }

    private void OnInitialized()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
