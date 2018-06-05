using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

    public void SetPos(GameObject obj)
    {
        transform.SetParent(obj.transform);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Transform goTrans = obj.GetComponent<PlayerControll>().CameraPlace;
        transform.position = goTrans.position;
    }
}
