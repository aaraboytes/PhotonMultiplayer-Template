using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SingletonScriptableObject<T> :ScriptableObject where T : ScriptableObject
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if(results.Length == 0)
                {
                    Debug.LogError("Results length is 0 of " + typeof(T).ToString());
                    return null;
                }
                if (results.Length > 1)
                {
                    Debug.LogError("Results length is greater than 1 of " + typeof(T).ToString());
                    return null;
                }
                instance = results[0];
                instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            }
            return instance;
        }
    }
}
