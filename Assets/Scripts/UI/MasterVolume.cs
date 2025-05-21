using UnityEngine;
using UnityEngine.Audio;

namespace UI
{
    public class MasterVolume : MonoBehaviour
    {
        private readonly float _minVolume = -80;
        private readonly float _maxVolume = 0;

        [SerializeField] private AudioMixerGroup _mixerGroup;

        private float _nowVolume;
        private string _soundType;

        private bool _isSaund = true;

        public void ToggleVolume()
        {
            if (!_isSaund)
                _mixerGroup.audioMixer.SetFloat("MasterVolume", _nowVolume);
            else
            {
                _mixerGroup.audioMixer.GetFloat("MasterVolume", out float volume);
                _mixerGroup.audioMixer.SetFloat("MasterVolume", _minVolume);
                _nowVolume = volume;
            }

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
}