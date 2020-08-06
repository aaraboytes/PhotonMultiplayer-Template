using Photon.Pun;
using UnityEngine;

public class RoomLeaver : MonoBehaviour
{
    private RoomsCanvases canvases;
    public void Initialize(RoomsCanvases canvases)
    {
        this.canvases = canvases;
    }
   public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        canvases.RoomDetails.Hide();
    }
}
