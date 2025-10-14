using UnityEngine;

namespace Characters.Base
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SurfaceDetector))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;
        private SurfaceDetector _surfaceDetector;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _surfaceDetector = GetComponent<SurfaceDetector>();
        }

        public bool IsRightOf(Transform target)
        {
            return _rigidbody.transform.position.x > target.position.x;
        }

        public void Move(float directionValue)
        {
            Vector2 velocity = _rigidbody.velocity;

            Vector3 direction = Vector3.right * directionValue;
            Vector3 alongSurfaceDirection = _surfaceDetector.Project(direction.normalized);

            Vector2 horizontalVelocity = alongSurfaceDirection * (_speed * Time.deltaTime);

            _rigidbody.velocity = new Vector2(horizontalVelocity.x, velocity.y);
        }
    
        public void MoveTowards(Transform target)
        {
            if (IsRightOf(target))
            {
                Move((float)Directions.Left);
            }
            else
            {
                Move((float)Directions.Right);
            }
        }
    }
}