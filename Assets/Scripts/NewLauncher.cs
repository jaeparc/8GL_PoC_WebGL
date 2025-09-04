using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Internal;

public class NewLauncher : MonoBehaviourPunCallbacks
{
    [Header("--- REFERENCES ---")]
    public GameObject LoginPanel;
    public LogManager LM;
    public Text Feedback;
    public InputField RoomID;
    public InputField MaxPlayers;

    [Header("--- SETTINGS ---")]
    public int MaxPlayersPerRoom = 4;

    bool _isConnecting;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void CreatingRoom()
    {
        Feedback.text = "Creating room...";
        string roomName = "R" + Random.Range(1000, 9999);
        if (int.TryParse(MaxPlayers.text, out int players))
            PhotonNetwork.CreateRoom(roomName, new Photon.Realtime.RoomOptions { MaxPlayers = players }, TypedLobby.Default);
        else
            PhotonNetwork.CreateRoom(roomName, new Photon.Realtime.RoomOptions { MaxPlayers = MaxPlayersPerRoom }, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Feedback.text = "Room created successfully";
    }

    public void Connect()
    {
        _isConnecting = true;
        LoginPanel.SetActive(false);
        Feedback.gameObject.SetActive(true);

        if (PhotonNetwork.IsConnected)
        {
            Feedback.text = "Joining room...";
            if (RoomID.text == "" || RoomID.text == null)
                CreatingRoom();
            else
                PhotonNetwork.JoinRoom(RoomID.text);
        }
        else
        {
            Feedback.text = "Connecting to server...";
            PhotonNetwork.NickName = LM.GetUsername();
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    void OnPlayFabLoginSuccess(LoginResult result)
    {
        Debug.Log("PlayFab login successful!");
    }

    void OnPlayFabLoginFailed(PlayFabError error)
    {
        Debug.LogError("PlayFab login failed: " + error.GenerateErrorReport());
    }

    public override void OnConnectedToMaster()
    {
        Feedback.text = "Connected to server";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Feedback.text = "Lobby joined";
        if (_isConnecting)
        {
            if (RoomID.text == "" || RoomID.text == null)
                CreatingRoom();
            else
                PhotonNetwork.JoinRoom(RoomID.text);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room join failed: " + message);
        CreatingRoom();
    }

    public override void OnJoinedRoom()
    {
        Feedback.text = "Room joined successfully";
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Feedback.text = "Loading level...";
            PhotonNetwork.LoadLevel("Playground");
        }
    }
}
