using System;
using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    [SerializeField] private int _amount;

    private void Awake()
    {
        if (_amount <= 0)
        {
            throw new ArgumentOutOfRangeException("Amount must be greater than zero");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IMedicinable medicinableComponent))
        {
            medicinableComponent.Treat(_amount);
        }
    }
}
