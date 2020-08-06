using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PauseMenu : MonoBehaviourPunCallbacks
{
    private bool isPaused = false;
    [SerializeField] GameObject _pausePanel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            _pausePanel.SetActive(isPaused);
        }
    }
    public void Continue()
    {
        isPaused = false;
        _pausePanel.SetActive(false);
    }
    public void Pause()
    {
        isPaused = true;
        _pausePanel.SetActive(true);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }
}
