using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private float _speed;
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private int _hp = 5;
    private Coroutine _myCor;

    public GameObject Target;
    
    private void Update()
    {
        if (Target == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(GameManager.Instance.MainTower.transform.position.x, 0, GameManager.Instance.MainTower.transform.position.z), _speed * Time.deltaTime);
            transform.LookAt(GameManager.Instance.MainTower.transform);
            _myCor = null;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.transform.position.x, 0, Target.transform.position.z), _speed * Time.deltaTime);
            transform.LookAt(Target.transform);
            if(_myCor == null)
            {
                _myCor = StartCoroutine(Fire());
            }
        }
    }

    public void Damage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
           _photonView.RPC("DestoyGO", PhotonTargets.All, null);
        }
        Debug.Log(_hp);

    }

    [PunRPC]
    private void DestoyGO()
    {
        Destroy(gameObject);
    }

    private IEnumerator Fire()
    {
        while(Target != null)
        {
            Target.GetComponent<PlayerControll>().HP--;
            yield return new WaitForSeconds(1f);
        }
    }
    
}
