using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SurfaceSlider))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private SurfaceSlider _surfaceSlider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _surfaceSlider = GetComponent<SurfaceSlider>();
    }

    public bool IsRightOf(Transform target)
    {
        return _rigidbody.transform.position.x > target.position.x;
    }
    
    public void Move(float directionValue)
    {
        Vector2 velocity = _rigidbody.velocity;
        
        Vector3 direction = Vector3.right * directionValue;
        Vector3 alongSurfaceDirection = _surfaceSlider.Project(direction.normalized);
        
        Vector2 horizontalVelocity = alongSurfaceDirection * (_speed * Time.deltaTime);

        _rigidbody.velocity = new Vector2(horizontalVelocity.x, velocity.y);
    }
}