using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomLister : MonoBehaviourPunCallbacks
{
    [SerializeField] VerticalScrollViewContent _content;
    [SerializeField] GameObject _prefab;
    private List<RoomListButton> elements = new List<RoomListButton>();
    private RoomsCanvases canvases;
    public void Initialize(RoomsCanvases canvases)
    {
        this.canvases = canvases;
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Updating room list");
        foreach(RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = elements.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(elements[index].gameObject);
                    elements.RemoveAt(index);
                }
            }
            else
            {
                int index = elements.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {
                    RoomListButton roomButton = Instantiate(_prefab, _content.transform).GetComponent<RoomListButton>();
                    roomButton.SetRoomInfo(info);
                    elements.Add(roomButton);
                }
            }
        }
        _content.UpdateSize();
        Debug.Log("Room list updated");
    }
    public override void OnJoinedRoom()
    {
        canvases.RoomDetails.Show();
        _content.transform.DestroyChildren();
        elements.Clear();
    }
}
