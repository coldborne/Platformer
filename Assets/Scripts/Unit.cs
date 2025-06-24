using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(ObjectDestroyer))]
public class Unit : MonoBehaviour, IDamageable, IMedicinable
{
    [SerializeField] private int _maxHealth;

    private Mover _mover;
    private Health _health;

    private ObjectDestroyer _objectDestroyer;

    protected virtual void Awake()
    {
        _mover = GetComponent<Mover>();
        _health = new Health(_maxHealth);

        _objectDestroyer = GetComponent<ObjectDestroyer>();
    }

    protected virtual void OnEnable()
    {
        _health.Died += Destroy;
    }

    protected virtual void OnDisable()
    {
        _health.Died -= Destroy;
    }

    public void Move(float horizontal)
    {
        _mover.Move(horizontal);
    }

    public bool IsRightOf(Transform target)
    {
        return _mover.IsRightOf(target);
    }

    public void TakeDamage(int amount)
    {
        _health.TakeDamage(amount);
    }

    public void Treat(int amount)
    {
        _health.Treat(amount);
    }

    private void Destroy()
    {
        _objectDestroyer.DestroyObject(gameObject);
    }
}