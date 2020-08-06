using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class RoomPlayerElement : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] RawImage _avatar;
    [SerializeField] GameObject _ready;
    public Player Player { get; private set; }
    public bool Ready = false;
    private void SetPlayerText(Player player)
    {
        int result = -1;
        if (player.CustomProperties.ContainsKey("CUSTOM_NUMBER"))
        {
            result = (int)player.CustomProperties["CUSTOM_NUMBER"];
        }
        _name.text = result.ToString() + " - " + player.NickName;
    }
    public void LoadAvatarPic(string url)
    {
        //Load avatar image
    }
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        SetPlayerText(player);
        SetReady(false);
    }
    public void SetReady(bool state)
    {
        _ready.SetActive(state);
        Ready = state;
    }
    #region PUN Callbacks
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(targetPlayer!=null && targetPlayer == Player)
        {
            if(changedProps.ContainsKey("CUSTOM_NUMBER"))
                SetPlayerText(targetPlayer);
        }
    }
    #endregion
}
