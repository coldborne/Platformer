using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Health _health;
    
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.minValue = _health.MinValue;
        _slider.maxValue = _health.MaxValue;
        
        Display(_health.Value);
    }

    private void OnEnable()
    {
        _health.ValueChanged += Display;
    }

    private void OnDisable()
    {
        _health.ValueChanged -= Display;
    }

    private void Display(int value)
    {
        _slider.value = value;
    }
}