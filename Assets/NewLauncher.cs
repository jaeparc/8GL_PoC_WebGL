using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class NewLauncher : MonoBehaviourPunCallbacks
{
    [Header("--- REFERENCES ---")]
    public GameObject LoginPanel;
    public Text Feedback;
    public InputField Username;
    public InputField RoomID;

    [Header("--- SETTINGS ---")]
    public int MaxPlayersPerRoom = 4;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void CreatingRoom()
    {
        Feedback.text = "Creating room...";
        string roomName = "R" + Random.Range(1000, 9999);
        PhotonNetwork.CreateRoom(roomName, new Photon.Realtime.RoomOptions { MaxPlayers = MaxPlayersPerRoom }, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Feedback.text = "Room created successfully";
    }

    public void Connect()
    {
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
            if (Username.text == "" || Username.text == null)
                PhotonNetwork.NickName = "Player#" + Random.Range(1000, 9999);
            else
                PhotonNetwork.NickName = Username.text;
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Feedback.text = "Connected to server";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Feedback.text = "Lobby joined";
        if (RoomID.text == "" || RoomID.text == null)
            CreatingRoom();
        else
            PhotonNetwork.JoinRoom(RoomID.text);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Feedback.text = "Room join failed: " + message;
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
    
    public override void OnLeftRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Log"); // ou le nom de ta scène de connexion
    }
}
