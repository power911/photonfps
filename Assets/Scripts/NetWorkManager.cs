using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NetWorkManager : Photon.PunBehaviour
{

    [SerializeField] private string _playerName;
    [SerializeField] private string _roomName;
    [SerializeField] private int _maxPlayers;
    [SerializeField] private int _maxBots;

    [SerializeField] private Button _buttonCreate;

    public string PlayerName { set { _playerName = value; PlayerNetwork.Instance.NickName = value; } }
    public string RoomName { set { _roomName = value; } }
    public int MaxPlayers { set { _maxPlayers = ++value; if (value > 1) { _buttonCreate.interactable = true; } else { _buttonCreate.interactable = false; } } }
    public string MaxBotsCount { set { _maxBots = Int32.Parse(value); } }
    
    public void Connecting()
    {
        PhotonNetwork.autoJoinLobby = true;
        MenuManager.Instance.ChangeCanvas(1);
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.playerName = _playerName;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.automaticallySyncScene = true;
        Debug.Log("Player Name: " + PhotonNetwork.playerName);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("You joined to lobby: " + PhotonNetwork.lobby);
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)_maxPlayers };

        if (PhotonNetwork.CreateRoom(_roomName, roomOptions, TypedLobby.Default))
        {
            print("create room successfully sent.");
        }
        else
        {
            print("create room failed to send");
        }
    }
}
   
