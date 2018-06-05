using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSettings : MonoBehaviour {
    
    [SerializeField] private Text _roomName;
    [SerializeField] private Button button;

    public bool Updated = false;
    public string RoomName;

    private void Start()
    {
        button.onClick.AddListener(()=>JoinRoom(RoomName));
        button.onClick.AddListener(() => MenuManager.Instance.PlayerLayoutGroup.GetComponent<PlayerLayoutGroup>().OnJoinedRoom());
    }

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void SetRoomName(string text)
    {
        RoomName = text;
        _roomName.text = RoomName;
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

}
