using UnityEngine;

public static class Transforms
{
    public static void DestroyChildren(this Transform t, bool destroyInmmediately = false)
    {
        foreach(Transform child in t)
        {
            if (destroyInmmediately)
                MonoBehaviour.DestroyImmediate(child.gameObject);
            else
                MonoBehaviour.Destroy(child.gameObject);
        }
    }
}