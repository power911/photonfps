using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject CurrentPlayer;
    public GameObject MainTower;
    public GameObject CameraObj;
    public Transform SpawnPlace;
    public Slider HpSlider;

    public int Shots;
    public int killedEnemy;


    [SerializeField] private Text _timeScore;
    [SerializeField] private Text _shots;
    [SerializeField] private Text _killedEnemy;
    [SerializeField] private Text _result;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private Transform[] _spawnPlaceEnemy;
    [SerializeField] private PhotonView _photonView;

    public enum Result
    {
        Winner,
        Looser
    }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
       GameObject obj =  PhotonNetwork.Instantiate("Player", SpawnPlace.position, Quaternion.identity, 0);
       CameraObj.GetComponent<CamController>().SetPos(obj);
       CurrentPlayer = obj;
       StartCoroutine(TimeScore());
    }

    public void EndGame(Result res)
    {
        _resultPanel.gameObject.SetActive(true);
        _killedEnemy.text = "Killed Enemy "+ killedEnemy.ToString();
        _shots.text = " Shots: " + Shots.ToString();
        switch (res)
        {
            case Result.Winner:
                {
                    _result.text = "YOU WINNER!!!";
                    break;
                }
            case Result.Looser:
                {
                    _result.text = "GAME OVER!!!";
                    break;
                }
        }
        StartCoroutine(End());
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }


    private IEnumerator SpawnEnemy()
    {
        if (PhotonNetwork.isMasterClient)
        {
            int counter = 0;
            while (counter < 100)
            {
                counter++;
                GameObject enemy = PhotonNetwork.Instantiate("Enemy", _spawnPlaceEnemy[UnityEngine.Random.Range(0, _spawnPlaceEnemy.Length)].position, Quaternion.identity, 0);
                yield return new WaitForSeconds(5f);
            }
        }
    }

    private IEnumerator TimeScore()
    {
        int temp = Int32.Parse(_timeScore.text);
        while (0 == temp)
        {
            temp--;
            _timeScore.text = temp.ToString();
            yield return new WaitForSeconds(1);
        }
        _timeScore.gameObject.SetActive(false);
        StartCoroutine(SpawnEnemy());
    }

    public void Destroy(GameObject go)
    {
        _photonView.RPC("RPC_Destoy", PhotonTargets.All, go);
    }

    [PunRPC]
    private void RPC_Destoy(GameObject go)
    {
        Destroy(go);
    }
}
