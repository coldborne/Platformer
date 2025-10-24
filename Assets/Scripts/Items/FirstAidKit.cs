using System;
using Interfaces;
using Logic;
using UnityEngine;

namespace Items
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ObjectDestroyer))]
    public class FirstAidKit : MonoBehaviour
    {
        [SerializeField] private int _amount;

        private ObjectDestroyer _objectDestroyer;

        private void Awake()
        {
            if (_amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Amount must be greater than zero");
            }

            _objectDestroyer = GetComponent<ObjectDestroyer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IMedicinable medicinableComponent))
            {
                medicinableComponent.Treat(_amount);
                _objectDestroyer.DestroyObject(gameObject);
            }
        }
    }
}