using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Scrollbar _volume;
    [SerializeField] private Toggle _mute;
    [SerializeField] private GameObject _buttonsForMobile;
    [SerializeField] private PlayerEngine _player;

    private readonly float _midleDifference = 1;
    private readonly float _lowDifference = 1.5f;
    private readonly float _hightDifference = 0.5f;
    private float _correntDifference;

    private void OnEnable()
    {
        if (!YandexGame.EnvironmentData.isDesktop)
            _buttonsForMobile.SetActive(true);
    }

    private void Start()
    {
        VolumeControl();
        SelectLanguage();
        SelectGameDifficulty();
    }

    private void OnDisable()
    {
        SaveVolume();
    }

    private void VolumeControl()
    {
        _volume.value = MainMenu.Volume;
        _mute.isOn = MainMenu.IsMute;
    }

    private void SelectLanguage()
    {
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(MainMenu.CorrectLanguage.ToString());
    }

    private void SelectGameDifficulty()
    {
        if (MainMenu.CorrectDifference == MainMenu.Difference.High)
            _correntDifference = _hightDifference;
        else if (MainMenu.CorrectDifference == MainMenu.Difference.Low)
            _correntDifference = _lowDifference;
        else
            _correntDifference = _midleDifference;

        _player.ChangeAccelerateSpeedFrequency(_correntDifference);
    }

    private void SaveVolume()
    {
        MainMenu.Volume = _volume.value;
        MainMenu.IsMute = _mute.isOn;
    }
}
