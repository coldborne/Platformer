using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Collection : IEnumerable
{
    [SerializeField] private List<Transform> _items;
    private int _currentIndex;

    public Collection(IEnumerable<Transform> items)
    {
        _items = new List<Transform>();
        _items.AddRange(items);
    }
    
    public Transform Current => _items[_currentIndex];
    
    public int Count => _items.Count;

    public Transform MoveNext()
    {
        _currentIndex = (_currentIndex + 1) % _items.Count;
        
        return Current;
    }

    public void Reset() => _currentIndex = 0;

    public IEnumerator<Transform> GetEnumerator() => _items.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
