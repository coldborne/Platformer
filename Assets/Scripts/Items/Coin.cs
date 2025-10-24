using UnityEngine;

namespace Items
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int _value;
    
        public int Value => _value;
    }
}
