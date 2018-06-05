using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : Photon.MonoBehaviour,IPunObservable
{
    public Transform CameraPlace;

    [SerializeField] private int _hp;
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private CharacterController _character;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage = 2;
    [SerializeField] private GameObject Ak_47;
    [SerializeField] private GameObject PM;

    public int HP { set { _hp = value; _hpSlider.value = value; } get { return _hp; } }

    private void Start()
    {
        _hpSlider = GameManager.Instance.HpSlider;
        _hpSlider.maxValue = _hp;
        _hpSlider.value = _hp;
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(0, mouseX * _rotSpeed, 0);
            Vector3 _moveDirection = new Vector3(horizontal * _speed, 0, vertical * _speed);
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection.y -= 20f * Time.deltaTime;
            _character.Move(_moveDirection * Time.deltaTime);
            if (Input.GetMouseButtonDown(0))
            {
                Shot();
            }
            SwapWeapron();
        }
    }

    private void Shot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 50f))
        {
            GameManager.Instance.Shots++;
            if (hit.transform.GetComponent<Enemy>())
            {
                hit.transform.GetComponent<Enemy>().Damage(_damage);
            }
        }
    }

    private void SwapWeapron()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            Ak_47.SetActive(true);
            PM.SetActive(false);
            _damage = 2;
        }
        if (Input.GetKey(KeyCode.F2))
        {
            Ak_47.SetActive(false);
            PM.SetActive(true);
            _damage = 1;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        stream.Serialize(ref pos);
        stream.Serialize(ref rot);
        if (stream.isReading)
        {
            transform.position = pos;
            transform.rotation = rot;
        }
    }

}
