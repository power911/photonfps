using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : Photon.PunBehaviour {

    public PhotonPlayer PhotonPlayer;

    [SerializeField] private Text _playerName;

    public void ApplyPhotonPlayer(PhotonPlayer photonPlayer)
    {
        _playerName.text = photonPlayer.NickName;
    }
}
