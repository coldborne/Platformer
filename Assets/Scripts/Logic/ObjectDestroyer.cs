using UnityEngine;

namespace Logic
{
    [DisallowMultipleComponent]
    public class ObjectDestroyer : MonoBehaviour
    {
        public void DestroyObject(GameObject gameObject)
        {
            Destroy(gameObject);
        }
    }
}