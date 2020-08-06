using Photon.Pun;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
    public static GameSettings GameSettings { get { return Instance._gameSettings; } }
    [SerializeField] GameSettings _gameSettings;
    [SerializeField] private List<NetworkedPrefab> networkedPrefabs = new List<NetworkedPrefab>();
    public static GameObject NetworkInstantiate(GameObject obj,Vector3 position, Quaternion rotation)
    {
        foreach (NetworkedPrefab networkedPrefab in Instance.networkedPrefabs)
        {
            if(networkedPrefab.Prefab == obj)
            {
                if (networkedPrefab.Path != string.Empty)
                {
                    GameObject result = PhotonNetwork.Instantiate(networkedPrefab.Path, position, rotation);
                    return result;
                }
                else
                {
                    Debug.LogError("Path is empty for gameobject name " + networkedPrefab.Prefab.name);
                    return null;
                }
            }
        }
        return null;
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void PopulateNetworkedPrefabs()
    {
#if UNITY_EDITOR
        Instance.networkedPrefabs.Clear();
        GameObject[] results = Resources.LoadAll<GameObject>("");
        for (int i = 0; i < results.Length; i++)
        {
            // TODO: Load prefabs by resources
            if (results[i].GetComponent<PhotonView>())
            {
                string path = AssetDatabase.GetAssetPath(results[i]);
                Instance.networkedPrefabs.Add(new NetworkedPrefab(results[i], path));
            }
        }
        for (int i = 0; i < Instance.networkedPrefabs.Count; i++)
        {
            UnityEngine.Debug.Log(Instance.networkedPrefabs[i].Prefab.name + " , " + Instance.networkedPrefabs[i].Prefab);

        }
#endif
    }
}
