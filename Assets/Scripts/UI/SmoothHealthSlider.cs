using System.Collections;
using Characters.Base;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [DefaultExecutionOrder(1)]
    [RequireComponent(typeof(Slider))]
    public class SmoothHealthSlider : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private float _speed;

        private IHealth _unitHealth;
        private Slider _slider;
        private Coroutine _changingValueCoroutine;

        private void Awake()
        {
            _slider = GetComponent<Slider>();

            _unitHealth = _unit.Health;
        
            _slider.interactable = false;
            _slider.minValue = _unitHealth.MinValue;
            _slider.maxValue = _unitHealth.MaxValue;

            _slider.value = _unitHealth.Value;
        }

        private void OnEnable()
        {
            _unitHealth.ValueChanged += Display;
        }

        private void OnDisable()
        {
            _unitHealth.ValueChanged -= Display;

            if (_changingValueCoroutine != null)
            {
                StopCoroutine(_changingValueCoroutine);
                _changingValueCoroutine = null;
            }
        }

        private void Display(float value)
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