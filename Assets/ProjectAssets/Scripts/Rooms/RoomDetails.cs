using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomDetails : MonoBehaviour
{
    [SerializeField] PlayersLister _playersLister;
    [SerializeField] RoomLeaver _roomLeaver;
    public RoomLeaver RoomLeaver { get { return _roomLeaver; } }
    private RoomsCanvases canvases;

    public void Initialize(RoomsCanvases canvases)
    {
        this.canvases = canvases;
        _playersLister.Initialize(canvases);
        _roomLeaver.Initialize(canvases);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}
