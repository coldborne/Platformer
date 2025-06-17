using System;
using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    private Vector3 _normal;

    public bool IsGrounded { get; private set; }

    public Vector3 Project(Vector3 direction)
    {
        return direction - Vector3.Dot(direction, _normal) * _normal;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsOnGround(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        IsOnGround(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
    }

    private void IsOnGround(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (Vector2.Angle(transform.up, contact.normal) < 45f)
            {
                IsGrounded = true;
            }
        }
    }
}