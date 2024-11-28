using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public sealed class SDKInitializer : MonoBehaviour
{
    //private void Awake()
    //{
    //      YandexGame.CallbackLogging = true;
    //}

    //private IEnumerator Start()
    //{
    //    yield return YandexGame.Initialize(OnInitialized);
    //}

    private void OnInitialized()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
