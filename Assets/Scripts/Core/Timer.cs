using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private readonly float _minRemainingTime;
    private readonly float _timeTick;

    private float _remainingTime;

    public event Action IsOver;

    public Timer()
    {
        _minRemainingTime = 0;
        _timeTick = 1;
    }

    public IEnumerator DoCountdown(float startRemainingTime)
    {
        _remainingTime = startRemainingTime;

        while (_remainingTime > _minRemainingTime)
        {
            yield return new WaitForSeconds(_timeTick);
            _remainingTime -= _timeTick;
        }

        _remainingTime = _minRemainingTime;
        IsOver?.Invoke();
    }
}