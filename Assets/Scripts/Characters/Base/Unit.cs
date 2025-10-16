using System;
using Interfaces;
using Logic;
using UnityEngine;

namespace Characters.Base
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Attacker))]
    [RequireComponent(typeof(ObjectDestroyer))]
    public class Unit : MonoBehaviour, IDamageable, IMedicinable
    {
        [SerializeField] private float _maxHealth;

        private Mover _mover;
        private ObjectDestroyer _objectDestroyer;

        public IHealth Health { get; private set; }

        protected virtual void Awake()
        {
            _mover = GetComponent<Mover>();
            Health = new Health(_maxHealth);

            _objectDestroyer = GetComponent<ObjectDestroyer>();
        }

        protected virtual void OnEnable()
        {
            Health.Died += Destroy;
        }

        protected virtual void OnDisable()
        {
            Health.Died -= Destroy;
        }

        public void Move(float horizontal)
        {
            _mover.Move(horizontal);
        }

        public void TakeDamage(float amount)
        {
            Health.TakeDamage(amount);
        }

        public void Treat(float amount)
        {
            Health.Treat(amount);
        }

        private void Destroy()
        {
            _objectDestroyer.DestroyObject(gameObject);
        }
    }
}