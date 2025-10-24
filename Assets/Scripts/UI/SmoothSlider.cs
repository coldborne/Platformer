using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [DefaultExecutionOrder(1)]
    [RequireComponent(typeof(Slider))]
    public abstract class SmoothSlider : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private Slider _slider;
        private Coroutine _changingValueCoroutine;

        protected virtual void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.interactable = false;
        }

        protected abstract void OnEnable();

        protected virtual void OnDisable()
        {
            if (_changingValueCoroutine != null)
            {
                StopCoroutine(_changingValueCoroutine);
                _changingValueCoroutine = null;
            }
        }

        protected void SetMinValue(float value)
        {
            _slider.minValue = value;
        }

        protected void SetMaxValue(float value)
        {
            _slider.maxValue = value;
        }

        protected void SetValue(float value)
        {
            _slider.value = value;
        }
        
        protected virtual void Display(float value)
        {
            if (_changingValueCoroutine != null)
            {
                StopCoroutine(_changingValueCoroutine);
            }

            _changingValueCoroutine = StartCoroutine(ChangingValue(value));
        }
        
        private IEnumerator ChangingValue(float targetValue)
        {
            while (Mathf.Approximately(_slider.value, targetValue) == false)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, targetValue, _speed * Time.unscaledDeltaTime);
                yield return null;
            }

            _slider.value = targetValue;
            _changingValueCoroutine = null;
        }
    }
}