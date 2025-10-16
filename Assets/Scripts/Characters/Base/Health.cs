using System;
using Interfaces;
using UnityEngine;

namespace Characters.Base
{
    public class Health : IHealth
    {
        public event Action Died;
        public event Action<float> ValueChanged;

        public Health(float maxValue)
        {
            MaxValue = maxValue;
            Value = MaxValue;
        }

        public float MinValue { get; } = 0.0f;
        public float MaxValue { get; }
        public float Value { get; private set; }

        public void TakeDamage(float amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount must be greater than zero");
            }

            float newValue = Value - amount;

            if (newValue > MinValue)
            {
                Value = newValue;
            }
            else
            {
                Value = MinValue;
                Died?.Invoke();
            }

            ValueChanged?.Invoke(Value);
        }

        public void Treat(float amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount must be greater than zero");
            }

            float newValue = Value + amount;

            Value = newValue < MaxValue ? newValue : MaxValue;

            ValueChanged?.Invoke(Value);
        }
    }
}