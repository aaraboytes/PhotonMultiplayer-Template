using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class RaiseEventExample : MonoBehaviourPun
{
    private const byte COLOR_CHANGE_EVENT = 0;
    private MeshRenderer renderer;
    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }
    private void Update()
    {
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            float hue = Random.value;
            ChangeColor(hue);
            object[] datas = new object[] {hue };
            PhotonNetwork.RaiseEvent(COLOR_CHANGE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
        }
    }
    private void ChangeColor(float hue)
    {
        renderer.material.color = Color.HSVToRGB(hue, 1,1);
    }
    private void NetworkingClient_EventReceived(EventData obj)
    {
        if(obj.Code == COLOR_CHANGE_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            float hue = (float)datas[0];
            ChangeColor(hue);
        }
    }
}
