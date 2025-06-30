using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MobileButtonResize : MonoBehaviour
    {
        private readonly float _learningTime = 30f;

        [SerializeField] private Image _button;

        private Vector3 _defaultSize = new Vector3(1f, 1f, 1f);
        private Vector3 _maxSize = new Vector3(2f, 2f, 2f);
        private Vector3 _step = new Vector3(0.05f, 0.05f, 0.05f);
        private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.03f);

        private void OnEnable()
        {
            if (Time.time < _learningTime)
                StartCoroutine(ChangeSize());
        }

        private IEnumerator ChangeSize()
        {
            bool isTimeGrow = true;
            int changesQuantity = 0;

            while (changesQuantity <= 2)
            {
                if (isTimeGrow)
                {
                    transform.localScale += _step;

                    if (transform.localScale.x >= _maxSize.x)
                    {
                        isTimeGrow = false;
                    }
                }
                else if (!isTimeGrow)
                {
                    transform.localScale -= _step;

                    if (transform.localScale.x <= _defaultSize.x)
                    {
                        isTimeGrow = true;
                        changesQuantity++;
                    }
                }

                if (changesQuantity >= 2)
                    StopCoroutine(ChangeSize());

                yield return _waitForSeconds;
            }
        }
    }
}