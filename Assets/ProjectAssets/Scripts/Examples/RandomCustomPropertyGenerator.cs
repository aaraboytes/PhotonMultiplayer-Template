using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomCustomPropertyGenerator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _customNumber;
    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
    private void OnDisable()
    {
        _myCustomProperties.Remove("CUSTOM_NUMBER");
        PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
    }
    public void SetCustomNumber()
    {
        int result = Random.Range(0, 100);
        _customNumber.text = result.ToString();
        _myCustomProperties["CUSTOM_NUMBER"] = result;
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
    }
}
