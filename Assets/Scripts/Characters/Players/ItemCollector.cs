using System;
using Items;
using Logic;
using UnityEngine;

namespace Characters.Players
{
    [RequireComponent(typeof(ObjectDestroyer))]
    public class ItemCollector : MonoBehaviour
    {
        private ObjectDestroyer _objectDestroyer;

        public event Action<int> CoinCollected;

        private void Awake()
        {
            _objectDestroyer = GetComponent<ObjectDestroyer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Coin coin))
            {
                CoinCollected?.Invoke(coin.Value);
                _objectDestroyer.DestroyObject(coin.gameObject);
            }
        }
    }
}