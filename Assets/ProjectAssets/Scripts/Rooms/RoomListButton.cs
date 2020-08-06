using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomListButton : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _playersCount;
    [SerializeField] Color _fullRoom;
    [SerializeField] Color _availableSpaceRoom;
    public Button Button { get { return _button; } }
    public RoomInfo RoomInfo { get; private set; }
    private bool isRoomFull;
    public void SetRoomInfo(RoomInfo info)
    {
        Debug.Log("Setting room info for room: " + info.Name);
        RoomInfo = info;
        _name.text = info.Name;
        _playersCount.text = info.PlayerCount + " / " + info.MaxPlayers;
        isRoomFull = info.PlayerCount == info.MaxPlayers;
        if (isRoomFull)
            _playersCount.color = _fullRoom;
        else
            _playersCount.color = _availableSpaceRoom;
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
