using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance;

    [SerializeField] private Canvas[] _canvas;

    [SerializeField] private LobbyCanvas _lobbyCanvas;
    public LobbyCanvas LobbyCanvas { get { return _lobbyCanvas; } }

    [SerializeField] private CurrentRoomCanvas _currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas { get { return _currentRoomCanvas; } }

    [SerializeField] private PlayerLayoutGroup _playerLayoutGroup;
    public PlayerLayoutGroup PlayerLayoutGroup { get { return _playerLayoutGroup; } }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else { Destroy(this); }
    }

    public void ChangeCanvas(int index)
    {
        for (int i = 0; i < _canvas.Length; i++)
        {
            _canvas[i].gameObject.SetActive(false);
        }
        _canvas[index].gameObject.SetActive(true);
    }


}
