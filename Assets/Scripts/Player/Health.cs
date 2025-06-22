using System;

public class Health : IDamageable, IMedicinable
{
    private readonly int _minValue;
    private readonly int _maxValue;

    public event Action Died;

    public Health(int maxValue)
    {
        if (maxValue <= _minValue)
        {
            throw new ArgumentOutOfRangeException("maxValue must be greater than zero");
        }

        _maxValue = maxValue;
        Value = _maxValue;
        _minValue = 0;
    }

    public int Value { get; private set; }

    public void TakeDamage(int amount)
    {
        if (amount <= _minValue)
        {
            throw new ArgumentOutOfRangeException("amount must be greater than zero");
        }

        int newValue = Value - amount;

        if (newValue > _minValue)
        {
            Value = newValue;
        }
        else
        {
            Value = _minValue;
            Died?.Invoke();
        }
    }

    public void Treat(int amount)
    {
        if (amount <= _minValue)
        {
            throw new ArgumentOutOfRangeException("amount must be greater than zero");
        }
        
        int newValue = Value + amount;
        
        Value = newValue < _maxValue ? newValue : _maxValue;
    }
}