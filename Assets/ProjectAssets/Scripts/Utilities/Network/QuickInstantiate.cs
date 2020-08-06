using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuickInstantiate : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] LocalPlayerHandler _playerHandler;
    private void Awake()
    {
        Vector3 offset = Random.insideUnitSphere * 3f + Vector3.up * 3f;
        Vector3 position = transform.position + offset;
        GameObject prefab = MasterManager.NetworkInstantiate(_prefab,position,Quaternion.identity);
        PlayerMovement movement = prefab.GetComponent<PlayerMovement>();
        movement.GiveHandler(_playerHandler);
    }
}
