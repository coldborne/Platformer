using System;

namespace Interfaces
{
    public interface IHealth : IDamageable, IMedicinable
    {
        public event Action Died;
        public event Action<float> ValueChanged;
        public float MinValue { get; }
        public float MaxValue { get; }
        public float Value { get; }
        public new void TakeDamage(float amount);
        public new void Treat(float amount);
    }
}