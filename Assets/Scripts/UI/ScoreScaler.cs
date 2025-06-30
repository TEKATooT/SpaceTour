using UnityEngine;

namespace UI
{
    public class ScoreScaler : MonoBehaviour
    {
        private readonly float _delay = 0.25f;

        private Vector3 _bigSize = new Vector3(2.5f, 2.5f, 2.5f);
        private Vector3 _lowSize = new Vector3(0.5f, 0.5f, 0.5f);

        private RectTransform _score;
        private float _timeIncrease;

        private void Start()
        {
            _score = GetComponent<RectTransform>();
        }

        private void Update()
        {
            Change();
        }

        public void Boosted()
        {
            _timeIncrease = Time.time + _delay;
        }

        private void Change()
        {
            if (_timeIncrease >= Time.time)
                _score.localScale = Vector3.Lerp(_score.localScale, _bigSize, Time.deltaTime);
            else
                _score.localScale = Vector3.Lerp(_score.localScale, _lowSize, Time.deltaTime);
        }
    }
}