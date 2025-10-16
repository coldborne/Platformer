using UnityEngine;

namespace Environment
{
    public class SurfaceDetector : MonoBehaviour
    {
        private readonly float _minForGroundAngle = 45f;

        private Vector3 _normal;

        public bool IsGrounded { get; private set; }

        public Vector3 Project(Vector3 direction)
        {
            return direction - Vector3.Dot(direction, _normal) * _normal;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsOnGround(collision, out ContactPoint2D contact))
            {
                _normal = contact.normal;
                IsGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            IsGrounded = false;
        }

        private bool IsOnGround(Collision2D collision, out ContactPoint2D ground)
        {
            ground = new ContactPoint2D();
        
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (Vector2.Angle(transform.up, contact.normal) < _minForGroundAngle)
                {
                    ground = contact;
                    return true;
                }
            }

            return false;
        }
    }
}