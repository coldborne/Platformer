using System;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private ObjectDestroyer _objectDestroyer;
    
    public event Action<int> CoinCollected;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            CoinCollected?.Invoke(coin.Value);
            _objectDestroyer.DestroyObject(coin.gameObject);
        }
    }
}
