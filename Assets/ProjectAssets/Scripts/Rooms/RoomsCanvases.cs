using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvases : MonoBehaviour
{
    [SerializeField] RoomDetails _roomDetails;
    [SerializeField] RoomCreator _roomCreator;
    [SerializeField] RoomLister _roomLister;
    public RoomCreator RoomCreator { get { return _roomCreator; } }
    public RoomLister RoomLister { get { return _roomLister; } }
    public RoomDetails RoomDetails { get { return _roomDetails; } }
    private void Awake()
    {
        FirstInitialize();
    }
    private void FirstInitialize()
    {
        _roomLister.Initialize(this);
        _roomCreator.Initialize(this);
        _roomDetails.Initialize(this);
    }
}
