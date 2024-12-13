using UnityEngine;
using UnityEngine.Audio;

public class MasterVolume : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;

    private string _soundType;
    private bool _isSaund = true;
    private float _minVolume = -80;
    private float _maxVolume = 0;
    private float _nowVolume;

    public void ToggleVolume()
    {
        if (!_isSaund)
            _mixerGroup.audioMixer.SetFloat("MasterVolume", _nowVolume);
        else
            _mixerGroup.audioMixer.SetFloat("MasterVolume", _minVolume);

        _isSaund = !_isSaund;
    }

    public void SetSoundType(string soundType)
    {
        _soundType = soundType;
    }

    public void VolumeChanges(float volume)
    {
        float nowVolume = _nowVolume;

        if (!_isSaund)
            _mixerGroup.audioMixer.SetFloat("MasterVolume", _minVolume);
        else
            _mixerGroup.audioMixer.SetFloat(_soundType, nowVolume = Mathf.Lerp(_minVolume, _maxVolume, volume));

        if (_soundType == "MasterVolume")
            _nowVolume = nowVolume;
    }

    public void PlaySaund(AudioSource audioSource)
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}