using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    [SerializeField] private int _hp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            _hp--;
            other.GetComponent<Enemy>().Damage(10);
            if (_hp <= 0)
            {
                GameManager.Instance.EndGame(GameManager.Result.Looser);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        int hp = _hp;
        stream.Serialize(ref hp);
        if (stream.isReading)
        {
            _hp = hp;
        }
    }
}
