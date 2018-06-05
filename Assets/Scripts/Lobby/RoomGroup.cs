using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGroup : Photon.PunBehaviour
{
    public string RoomName;

    [SerializeField] private GameObject _roomPrefab;

    private List<RoomSettings> _roomSettings = new List<RoomSettings>();

    public override void OnReceivedRoomListUpdate()
    {
        RoomInfo[] room = PhotonNetwork.GetRoomList();
        foreach(RoomInfo rooms in room)
        {
            RoomReceived(rooms);
        }
        RemoveOldRooms();
    }

    private  void RoomReceived(RoomInfo room)
    {
        int index = _roomSettings.FindIndex(x => x.RoomName == room.Name);
        if (index == -1 || index == -2)
        {
            if(room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                GameObject roomObj = Instantiate(_roomPrefab);
                roomObj.transform.SetParent(transform, false);

                RoomSettings roomSettings = roomObj.GetComponent<RoomSettings>();
                _roomSettings.Add(roomSettings);
                index = (_roomSettings.Count - 1);
            }
        }
        if(index != -1)
        {
            RoomSettings roomSettings = _roomSettings[index];
            roomSettings.SetRoomName(room.Name);
            roomSettings.Updated = true;
        }
    }

    private void RemoveOldRooms()
    {
        List<RoomSettings> removeRooms = new List<RoomSettings>();
        foreach(RoomSettings room in _roomSettings)
        {
            if (!room.Updated)
            {
                removeRooms.Add(room);
            }
            else
            {
                room.Updated = false;
            }
        }
        foreach(RoomSettings room in removeRooms)
        {
            GameObject roomObj = room.gameObject;
            _roomSettings.Remove(room);
            Destroy(roomObj);
        }
        
    }
}
