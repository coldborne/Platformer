using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface ICoroutine
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(IEnumerator routine);
    }
}