using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    public class Timer
    {
        private readonly float _minRemainingTime;
        private readonly float _timeTick;
        
        private WaitForSeconds _waitForSeconds;

        public event Action IsOver;

        public Timer()
        {
            _minRemainingTime = 0;
            _timeTick = 1;
            _waitForSeconds = new WaitForSeconds(_timeTick);
        }
        
        public float RemainingTime { get; private set; }

        public IEnumerator DoCountdown(float startRemainingTime)
        {
            RemainingTime = startRemainingTime;

            while (RemainingTime > _minRemainingTime)
            {
                yield return _waitForSeconds;
                RemainingTime -= _timeTick;
            }

            RemainingTime = _minRemainingTime;
            IsOver?.Invoke();
        }
    }
}