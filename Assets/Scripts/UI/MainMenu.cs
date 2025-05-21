using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        static public bool IsMute;
        static public float Volume;
        static public Language CorrectLanguage;
        static public Difference CorrectDifference;

        static private bool s_isFirstStart = true;

        private const string EnglishYaCode = "en";
        private const string RussianYaCode = "ru";
        private const string TurkishYaCode = "tr";

        private const int First = 0;
        private const int Second = 1;
        private const int Third = 2;

        private readonly float _normalTime = 1f;
        private readonly float _stopTime = 0f;

        [SerializeField] private Scrollbar _volume;
        [SerializeField] private Toggle _mute;

        [SerializeField] private TMP_Dropdown _selectLanguageDropBar;
        [SerializeField] private TMP_Dropdown _selectDifferenceDropBar;

        [SerializeField] private LeaderboardYG _leaderboardYG;

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
            if (!s_isFirstStart)
            {
                VolumeCorrect();
                OnSelectLanguageDropBar((int)CorrectLanguage);
                OnSelectDifferenceDropBar((int)CorrectDifference);
            }
            else
            {
                ApplyYandexLanguage();
                OnSelectDifferenceDropBar(Second);

                s_isFirstStart = false;
            }
        }

        private void OnDisable()
        {
            YandexGame.onVisibilityWindowGame -= OnVisibilityWindowGame;
        }

        private void VolumeCorrect()
        {
            _volume.value = Volume;
            _mute.isOn = IsMute;
        }

        private void ApplyYandexLanguage()
        {
            if (YandexGame.EnvironmentData.language == TurkishYaCode)
                OnSelectLanguageDropBar(Third);
            else if (YandexGame.EnvironmentData.language == RussianYaCode)
                OnSelectLanguageDropBar(Second);
            else
                OnSelectLanguageDropBar(First);
        }

        private void SetLanguage(Language language, int index)
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll(language.ToString());
            CorrectLanguage = language;
            _selectLanguageDropBar.value = index;
        }

        private void SetDifference(Difference difference, int index)
        {
            _leaderboardYG.SetNameLB(difference.ToString());
            CorrectDifference = difference;
            _selectDifferenceDropBar.value = index;
        }

        public void OnSelectLanguageDropBar(int index)
        {
            switch (index)
            {
                case First:
                    SetLanguage(Language.English, First);
                    break;
                case Second:
                    SetLanguage(Language.Russian, Second);
                    break;
                case Third:
                    SetLanguage(Language.Turkish, Third);
                    break;
                default:
                    break;
            }
        }

        public void OnSelectDifferenceDropBar(int index)
        {
            switch (index)
            {
                case First:
                    SetDifference(Difference.Low, First);
                    break;
                case Second:
                    SetDifference(Difference.Midle, Second);
                    break;
                case Third:
                    SetDifference(Difference.High, Third);
                    break;
                default:
                    break;
            }

            _leaderboardYG.UpdateLB();
        }

        public void OnStartGameButton()
        {
            Volume = _volume.value;
            IsMute = _mute.isOn;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + Second);
        }

        private void OnVisibilityWindowGame(bool isVisible)
        {
            if (isVisible)
            {
                AudioListener.pause = false;
                Time.timeScale = _normalTime;
            }
            else
            {
                AudioListener.pause = true;
                Time.timeScale = _stopTime;
            }
        }
    }
}