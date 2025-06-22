using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(ItemCollector))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamageable, IMedicinable 
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private int _maxHealth;
    
    private Health _health;
    private Mover _mover;
    private Jumper _jumper;
    private ItemCollector _itemCollector;

    private int _money;

    private void Awake()
    {
        _health = new Health(_maxHealth);
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _itemCollector = GetComponent<ItemCollector>();
    }

    private void OnEnable()
    {
        _inputReader.MoveButtonPressed += Move;
        _inputReader.JumpButtonPressed += Jump;

        _itemCollector.CoinCollected += IncreaseMoney;
    }

    private void OnDisable()
    {
        _inputReader.MoveButtonPressed -= Move;
        _inputReader.JumpButtonPressed -= Jump;

        _itemCollector.CoinCollected -= IncreaseMoney;
    }

    public void TakeDamage(int amount)
    {
        _health.TakeDamage(amount);
    }

    public void Treat(int amount)
    {
        _health.Treat(amount);
    }

    private void Move(float horizontal)
    {
        _mover.Move(horizontal);
    }

    private void Jump()
    {
        _jumper.Jump();
    }

    private void IncreaseMoney(int value)
    {
        _money += value;
    }
}