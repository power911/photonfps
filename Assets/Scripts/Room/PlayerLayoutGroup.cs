using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayoutGroup : Photon.PunBehaviour {

    [SerializeField] private GameObject _playerPrefab;

    [SerializeField]private List<PlayerListing> _playerListing = new List<PlayerListing>();

    public override  void OnJoinedRoom ()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        MenuManager.Instance.ChangeCanvas(2);
        
        PhotonPlayer[] photonPlayer = PhotonNetwork.playerList;
        for (int i = 0; i < photonPlayer.Length; i++)
        {
            PlayerJoinedRoom(photonPlayer[i]);
        }
    }

    public override void OnPhotonPlayerActivityChanged(PhotonPlayer photonPlayer)
    {
        PlayerJoinedRoom(photonPlayer);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer photonPlayer)
    {
        PlayerJoinedRoom(photonPlayer);
    }

    public void PlayerJoinedRoom(PhotonPlayer photonPlayer)
    {
        if (photonPlayer == null)
            return;

        PlayerLeftRoom(photonPlayer);
        GameObject playerListingObj = Instantiate(_playerPrefab);
        playerListingObj.transform.SetParent(transform, false);
        PlayerListing playerListing = playerListingObj.GetComponent<PlayerListing>();
        playerListing.ApplyPhotonPlayer(photonPlayer);
        _playerListing.Add(playerListing);
    }

    public void PlayerLeftRoom(PhotonPlayer photonPlayer)
    {
        int index = _playerListing.FindIndex(x => x.PhotonPlayer == photonPlayer);
        if(index != -1)
        {
            Destroy(_playerListing[index].gameObject);
            _playerListing.RemoveAt(index);
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
    {
        PlayerLeftRoom(photonPlayer);
        if (!photonPlayer.IsMasterClient)
        {
            MenuManager.Instance.ChangeCanvas(1);
        }
        Debug.Log("disconnect");
    }

    public void OnClickLeaveRoom()
    {
        MenuManager.Instance.ChangeCanvas(1);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
       PlayerLeftRoom(newMasterClient);
       PhotonNetwork.LeaveRoom();
    }

}
