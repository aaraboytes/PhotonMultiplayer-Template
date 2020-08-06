using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayersLister : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI _roomName;
    [SerializeField] Toggle _isReadyToggle;
    [SerializeField] TextMeshProUGUI _readyOrStart;
    [SerializeField] VerticalScrollViewContent _content;
    [SerializeField] GameObject _prefab;
    private bool isReady = false;
    private List<RoomPlayerElement> elements = new List<RoomPlayerElement>();
    private RoomsCanvases canvases;
    public override void OnEnable()
    {
        base.OnEnable();
        GetCurrentRoomPlayers();
        SetReady(false);
    }
    /*public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < elements.Count; i++)
        {
            Destroy(elements[i].gameObject);
        }
        elements.Clear();
    }*/
    private void AddPlayerListing(Player player)
    {
        int index = elements.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            elements[index].SetPlayerInfo(player);
        }
        else
        {
            RoomPlayerElement playerElement = Instantiate(_prefab, _content.transform).GetComponent<RoomPlayerElement>();
            playerElement.SetPlayerInfo(player);
            elements.Add(playerElement);
        }
    }
    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected || PhotonNetwork.CurrentRoom == null|| PhotonNetwork.CurrentRoom.Players == null)
            return;
        foreach(KeyValuePair<int,Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }
    private void SetReady(bool state)
    {
        isReady = state;
        _isReadyToggle.isOn = state;
    }
    private void SetReadyButton(bool isMasterClient)
    {
        _isReadyToggle.gameObject.SetActive(!isMasterClient);
        _readyOrStart.text = isMasterClient ? "START" : "READY";
    }
    public void Initialize(RoomsCanvases canvases)
    {
        this.canvases = canvases;
    }
    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //for (int i = 0; i < elements.Count; i++)
            //{
            //    if(elements[i].Player!=PhotonNetwork.LocalPlayer)
            //    {
            //        if (!elements[i].Ready)
            //            return;
            //    }
            //}

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
        else
        {
            SetReady(!isReady);
            base.photonView.RPC("RPC_ChangeReadyState",RpcTarget.MasterClient,PhotonNetwork.LocalPlayer, isReady);
        }
    }

    #region Pun Callbacks
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined to a room");
        SetReadyButton(PhotonNetwork.IsMasterClient);
        _roomName.text = PhotonNetwork.CurrentRoom.Name;
        GetCurrentRoomPlayers();
        canvases.RoomDetails.Show();
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        canvases.RoomDetails.RoomLeaver.LeaveRoom();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
        Debug.Log("New player entered to the room : " + newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = elements.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(elements[index].gameObject);
            elements.RemoveAt(index);
        }
        Debug.Log(otherPlayer.NickName + " left the room");
    }
    #endregion

    #region RPC
    [PunRPC]
    private void RPC_ChangeReadyState(Player player,bool ready)
    {
        int index = elements.FindIndex(x => x.Player == player);
        if (index != -1)
            elements[index].SetPlayerInfo(player);
        elements[index].SetReady(ready);
    }
    #endregion
}
