using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        ///<summary>
        ///Authentification values should have a unique ID for the player, this will be 
        ///changed using the API user id.
        ///</summary>
        //AuthenticationValues authValues = new AuthenticationValues("1");
        //PhotonNetwork.AuthValues = authValues;
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 10;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting photon network...");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName+" is being connected to lobby...");
        if(!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason " + cause.ToString());
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined to lobby");
        ///<summary>
        ///The string array should have the userIds, this is gonna update the friend
        ///list and call the OnFriendListUpdate
        ///</summary>
        //PhotonNetwork.FindFriends(new string[] {"1"});
    }
    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        foreach (FriendInfo friend in friendList)
        {
            Debug.Log(friend.IsOnline?"Online":"Offline" + ": " + friend.UserId + " " + friend.UserId);
        }
    }
}
