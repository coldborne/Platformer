using System.Collections;
using Characters.Base;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    
    public class SmoothHealthSlider : SmoothSlider
    {
        [SerializeField] private Unit _unit;

        private IHealth _unitHealth;
        
        protected override void Awake()
        {
            base.Awake();
            
            _unitHealth = _unit.Health;
            SetMinValue(_unitHealth.MinValue);
            SetMaxValue(_unitHealth.MaxValue);
            SetValue(_unitHealth.Value);
        }

        protected override void OnEnable()
        {
            _unitHealth.ValueChanged += Display;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _unitHealth.ValueChanged -= Display;
        }
    }
}