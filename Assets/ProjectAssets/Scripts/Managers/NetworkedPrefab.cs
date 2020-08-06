using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class NetworkedPrefab
{
    public GameObject Prefab;
    public string Path;
    public NetworkedPrefab(GameObject prefab, string path)
    {
        Prefab = prefab;
        Path = GetFixedPath(path);
    }
    private string GetFixedPath(string path)
    {
        int extensionLength = System.IO.Path.GetExtension(path).Length;
        int additionalLength = 10;
        int startIndex = path.IndexOf("Resources");
        if (startIndex == -1)
        {
            Debug.LogError("Start index not found");
            return string.Empty;
        }
        else
            return path.Substring(startIndex + additionalLength, path.Length - (additionalLength + startIndex + extensionLength));
    }
}
