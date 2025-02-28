using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Scrollbar _volume;
        [SerializeField] private Toggle _mute;

        [SerializeField] private TMP_Dropdown _selectLanguageDropBar;
        [SerializeField] private TMP_Dropdown _selectDifferenceDropBar;

        [SerializeField] private LeaderboardYG _leaderboardYG;

        static public bool IsMute;
        static public float Volume;
        static public Language CorrectLanguage;
        static public Difference CorrectDifference;

        static private bool _isFirstStart = true;

        private const string EnglishYaCode = "en";
        private const string RussianYaCode = "ru";
        private const string TurkishYaCode = "tr";

        private const int First = 0;
        private const int Second = 1;
        private const int Third = 2;

        private readonly float _normalTime = 1f;
        private readonly float _stopTime = 0f;

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
            if (!_isFirstStart)
            {
                VolumeCorrect();
                SelectLanguageDropBar((int)CorrectLanguage);
                SelectDifferenceDropBar((int)CorrectDifference);
            }
            else
            {
                ApplyYandexLanguage();
                SelectDifferenceDropBar(Second);

                _isFirstStart = false;
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
                Time.timeScale = _normalTime;
            }
            else
            {
                AudioListener.pause = true;
                Time.timeScale = _stopTime;
            }
        }

        public void StartGameButton()
        {
            Volume = _volume.value;
            IsMute = _mute.isOn;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + Second);
        }

        public void ApplyYandexLanguage()
        {
            if (YandexGame.EnvironmentData.language == TurkishYaCode)
            {
                SelectLanguageDropBar(Third);
            }
            else if (YandexGame.EnvironmentData.language == RussianYaCode)
            {
                SelectLanguageDropBar(Second);
            }
            else
            {
                SelectLanguageDropBar(First);
            }
        }

        public void SelectLanguageDropBar(int index)
        {
            switch (index)
            {
                case First:
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll(Language.English.ToString());
                    CorrectLanguage = Language.English;
                    _selectLanguageDropBar.value = First;
                    break;
                case Second:
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll(Language.Russian.ToString());
                    CorrectLanguage = Language.Russian;
                    _selectLanguageDropBar.value = Second;
                    break;
                case Third:
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll(Language.Turkish.ToString());
                    CorrectLanguage = Language.Turkish;
                    _selectLanguageDropBar.value = Third;
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
                    _leaderboardYG.SetNameLB(Difference.Low.ToString());
                    CorrectDifference = Difference.Low;
                    _selectDifferenceDropBar.value = First;
                    break;
                case Second:
                    _leaderboardYG.SetNameLB(Difference.Midle.ToString());
                    CorrectDifference = Difference.Midle;
                    _selectDifferenceDropBar.value = Second;
                    break;
                case Third:
                    _leaderboardYG.SetNameLB(Difference.High.ToString());
                    CorrectDifference = Difference.High;
                    _selectDifferenceDropBar.value = Third;
                    break;
                default:
                    break;
            }

            _leaderboardYG.UpdateLB();
        }

        private void VolumeCorrect()
        {
            _volume.value = Volume;
            _mute.isOn = IsMute;
        }
    }
}