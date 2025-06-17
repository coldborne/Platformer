using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SurfaceSlider))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _height;
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private SurfaceSlider _surfaceSlider;

    private bool _isJumping;
    private Coroutine _jumpCoroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _surfaceSlider = GetComponent<SurfaceSlider>();
    }

    public void Jump()
    {
        if (_isJumping == false && _surfaceSlider.IsGrounded)
        {
            _jumpCoroutine = StartCoroutine(Jumping());
        }
    }

    private IEnumerator Jumping()
    {
        _isJumping = true;
        _rigidbody.velocity = Vector2.zero;

        Vector3 start = transform.position;
        Vector3 target = start + transform.up * _height;

        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            yield return null;
        }

        _isJumping = false;
    }
}