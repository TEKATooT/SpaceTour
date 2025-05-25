namespace UI
{
    using UnityEngine;
    using UnityEngine.Audio;

    public class MasterVolume : MonoBehaviour
    {
        private const string MaestroVolume = "MasterVolume";

        private readonly float _minVolume = -80;
        private readonly float _maxVolume = 0;

        [SerializeField] private AudioMixerGroup _mixerGroup;

        private float _nowVolume;
        private string _soundType;

        private bool _isSaund = true;

        public void PlaySaund(AudioSource audioSource)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }

        public void SetSoundType(string soundType)
        {
            _soundType = soundType;
        }

        public void OnVolumeChanges(float volume)
        {
            float nowVolume = _nowVolume;

            if (!_isSaund)
                _mixerGroup.audioMixer.SetFloat(MaestroVolume, _minVolume);
            else
                _mixerGroup.audioMixer.SetFloat(_soundType, nowVolume = Mathf.Lerp(_minVolume, _maxVolume, volume));

            if (_soundType == MaestroVolume)
                _nowVolume = nowVolume;
        }

        public void OnVolumeToggle()
        {
            if (!_isSaund)
            {
                _mixerGroup.audioMixer.SetFloat(MaestroVolume, _nowVolume);
            }
            else
            {
                _mixerGroup.audioMixer.GetFloat(MaestroVolume, out float volume);
                _mixerGroup.audioMixer.SetFloat(MaestroVolume, _minVolume);
                _nowVolume = volume;
            }

            _isSaund = !_isSaund;
        }
    }
}