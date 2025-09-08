using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
[RequireComponent(typeof(Slider))]
public class SmoothHealthSlider : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private float _speed;

    private Slider _slider;
    private Coroutine _changingValueCoroutine;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        
        _slider.interactable = false;
        _slider.minValue = _unit.MinHealth;
        _slider.maxValue = _unit.MaxHealth;

        _slider.value = _unit.Health;
    }

    private void OnEnable()
    {
        _unit.HealthChanged += Display;
    }

    private void OnDisable()
    {
        _unit.HealthChanged -= Display;

        StopAllCoroutines();
        
        _changingValueCoroutine = null;
    }

    private void Display(int value)
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