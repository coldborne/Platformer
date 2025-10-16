using System.Collections;
using Environment;
using UnityEngine;

namespace Characters.Base
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SurfaceDetector))]
    public class Jumper : MonoBehaviour
    {
        [SerializeField] private float _height;
        [SerializeField] private float _speed;

        [SerializeField, Min(1f)] private float _arrivalThreshold;

        private Rigidbody2D _rigidbody;
        private SurfaceDetector _surfaceDetector;

        private bool _isJumping;
        private Coroutine _jumpCoroutine;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _surfaceDetector = GetComponent<SurfaceDetector>();
        }

        public void Jump()
        {
            if (_isJumping == false && _surfaceDetector.IsGrounded)
            {
                _jumpCoroutine = StartCoroutine(Jumping());
            }
        }

        private IEnumerator Jumping()
        {
            _isJumping = true;
            _rigidbody.velocity = Vector2.zero;

            Vector3 start = transform.position;
            Vector3 targetPosition = start + transform.up * _height;

            float sqrArrivalThreshold = _arrivalThreshold * _arrivalThreshold;

            while ((transform.position - targetPosition).sqrMagnitude > sqrArrivalThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
                yield return null;
            }

            _isJumping = false;
        }
    }
}