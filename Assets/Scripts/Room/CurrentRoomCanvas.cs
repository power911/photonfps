using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRoomCanvas : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private PhotonView PhotonView;

    public void OnClickStartSync()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonView.RPC("loadlevel", PhotonTargets.MasterClient, null);
            PhotonView.RPC("loadlevel", PhotonTargets.Others, null);
        }
    }

    [PunRPC]
    private void loadlevel()
    {
        PhotonNetwork.LoadLevel(1);
    }

}
