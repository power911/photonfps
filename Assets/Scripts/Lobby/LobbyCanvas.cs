using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour {

    [SerializeField] private RoomGroup _roomGroup;

    public void OnClick_JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
