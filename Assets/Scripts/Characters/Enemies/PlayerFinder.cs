using System;
using System.Collections;
using Characters.Players;
using UnityEngine;

namespace Characters.Enemies
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerFinder : MonoBehaviour
    {
        [SerializeField] private float _loseTargetDelay;

        private WaitForSeconds _waitForSeconds;
        private Coroutine _losePlayerCoroutine;

        public event Action PlayerFound;
        public event Action PlayerLost;

        public Vector2? PlayerPosition { get; private set; }

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_loseTargetDelay);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<Player>(out _))
            {
                PlayerPosition = collider.transform.position;

                if (_losePlayerCoroutine != null)
                {
                    StopCoroutine(_losePlayerCoroutine);
                    _losePlayerCoroutine = null;
                }

                PlayerFound?.Invoke();
            }
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.TryGetComponent<Player>(out _))
            {
                PlayerPosition = collider.transform.position;
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.TryGetComponent<Player>(out _))
            {
                _losePlayerCoroutine = StartCoroutine(LosePlayerCountDown());
            }
        }

        private IEnumerator LosePlayerCountDown()
        {
            yield return _waitForSeconds;

            PlayerPosition = null;
            PlayerLost?.Invoke();
        }
    }
}