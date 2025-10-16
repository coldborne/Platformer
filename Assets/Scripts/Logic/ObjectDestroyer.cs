using UnityEngine;

namespace Logic
{
    public class ObjectDestroyer : MonoBehaviour
    {
        public void DestroyObject(GameObject gameObject)
        {
            Destroy(gameObject);
        }
    }
}