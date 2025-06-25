using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public void DestroyObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}