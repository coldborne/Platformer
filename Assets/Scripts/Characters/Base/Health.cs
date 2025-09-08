using System;

public class Health : IDamageable, IMedicinable
{
    public event Action Died;
    public event Action<int> ValueChanged;

    public Health(int maxValue)
    {
        MaxValue = maxValue;
        Value = MaxValue;
    }

    public int MinValue { get; } = 0;
    public int MaxValue { get; }
    public int Value { get; private set; }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("amount must be greater than zero");
        }

        int newValue = Value - amount;

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

    public void Treat(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("amount must be greater than zero");
        }

        int newValue = Value + amount;

        Value = newValue < MaxValue ? newValue : MaxValue;

        ValueChanged?.Invoke(Value);
    }
}