using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace GameControl
{
    public sealed class SDKInitializer : MonoBehaviour
    {
        private void Awake()
        {
            YandexGame.GetDataEvent += OnInitialized;
        }

        private void OnInitialized()
        {
            YandexGame.GetDataEvent -= OnInitialized;

            YandexGame.GameplayStart();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}