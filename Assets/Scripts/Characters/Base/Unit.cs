using System;
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

    public event Action<int> HealthChanged;

    public int MinHealth => _health.MinValue;
    public int MaxHealth => _health.MaxValue;
    public int Health => _health.Value;

    protected virtual void Awake()
    {
        _mover = GetComponent<Mover>();
        _health = new Health(_maxHealth);

        _objectDestroyer = GetComponent<ObjectDestroyer>();
    }

    protected virtual void OnEnable()
    {
        _health.Died += Destroy;
        _health.ValueChanged += ChangeHealth;
    }

    protected virtual void OnDisable()
    {
        _health.Died -= Destroy;
        _health.ValueChanged -= ChangeHealth;
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

    private void ChangeHealth(int value)
    {
        HealthChanged?.Invoke(value);
    }

    private void Destroy()
    {
        _objectDestroyer.DestroyObject(gameObject);
    }
}