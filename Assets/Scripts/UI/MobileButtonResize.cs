using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MobileButtonResize : MonoBehaviour
    {
        [SerializeField] private Image _button;
        private Vector3 _defaultSize;
        private Vector3 _maxSize;
        private float _doubleMultiplication = 2f;

        private float _durationTime = 6f;
        private bool _isTimeGrow = true;
        private float _timer = 0f;

        private readonly float _one = 1f;
        private readonly float _zero = 0f;

        private void OnEnable()
        {
            _defaultSize = _button.rectTransform.localScale;

            _maxSize = new Vector3(_defaultSize.x, _defaultSize.y, _defaultSize.z) * _doubleMultiplication;
        }

        private void Update()
        {
            if (_durationTime >= Time.time)
                ChangeSize();
        }

        private void ChangeSize()
        {
            if (_isTimeGrow)
            {
                _button.rectTransform.localScale = Vector3.Lerp(_button.rectTransform.localScale, _maxSize, Time.deltaTime);

                _timer += Time.deltaTime;

                if (_timer > _one)
                {
                    _isTimeGrow = false;

                    _timer = _zero;
                }
            }
            else if (_isTimeGrow == false)
            {
                _button.rectTransform.localScale = Vector3.Lerp(_button.rectTransform.localScale, _defaultSize, Time.deltaTime);

                _timer += Time.deltaTime;

                if (_timer > _one)
                {
                    _isTimeGrow = true;

                    _timer = _zero;
                }
            }
        }
    }
}
