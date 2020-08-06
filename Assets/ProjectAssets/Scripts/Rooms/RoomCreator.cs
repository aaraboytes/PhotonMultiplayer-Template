using Photon.Pun;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomCreator : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField _roomName;
    [SerializeField] GameObject _roomCreatedPanel;
    private RoomsCanvases canvases;
    
    private void Start()
    {
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }
    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Photon network is not connected yet");
            return;
        }
        Debug.Log("Creating room...");
        RoomOptions options = new RoomOptions()
        {
            MaxPlayers = 4,
            BroadcastPropsChangeToAll = true,
            PublishUserId = true
        };
        PhotonNetwork.JoinOrCreateRoom(_roomName.text,options,TypedLobby.Default);
    }
    public void Initialize(RoomsCanvases canvases)
    {
        this.canvases = canvases;
    }

    #region PUNCallbacks
    public override void OnCreatedRoom()
    {
        Debug.Log("Room created successfully");
        canvases.RoomDetails.Show();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: "+message);
        base.OnCreateRoomFailed(returnCode, message);
    }
    #endregion
}
