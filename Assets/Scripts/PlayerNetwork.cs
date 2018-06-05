using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : Photon.PunBehaviour {
    public static PlayerNetwork Instance;

    [SerializeField] private string _nickName;
    public string NickName { get {return _nickName; } set { _nickName = value; } }


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
