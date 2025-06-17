using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(ItemCollector))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private Mover _mover;
    private Jumper _jumper;
    private ItemCollector _itemCollector;

    private int _money;

    private void Awake()
    {
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