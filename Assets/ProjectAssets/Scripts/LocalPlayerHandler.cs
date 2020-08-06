using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LocalPlayerHandler : MonoBehaviourPunCallbacks
{
    public PlayerMovement Player { get { return player; } }
    private PlayerMovement player;
    public void SetPlayer(PlayerMovement _player)
    {
        player = _player;
    }
}
