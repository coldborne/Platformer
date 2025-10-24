using Characters.Players;
using UnityEngine;

namespace UI
{
    public class SmoothAbilitySlider : SmoothSlider
    {
        [SerializeField] private Player _player;

        protected override void OnEnable()
        {
            _player.AbilityTimeChanged += Display;
            _player.AbilityStarted += SetNewValues;
            _player.AbilityCooldownStarted += SetNewValues;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _player.AbilityTimeChanged -= Display;
            _player.AbilityStarted -= SetNewValues;
            _player.AbilityCooldownStarted -= SetNewValues;
        }

        private void SetNewValues(float startValue, float endValue, bool isAscending)
        {
            SetMinValue(startValue);
            SetMaxValue(endValue);

            float value = isAscending ? startValue : endValue;

            SetValue(value);
        }
    }
}