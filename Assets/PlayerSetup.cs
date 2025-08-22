using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerSetup : MonoBehaviourPun
{
    public GameObject Cinemachine;
    public TMP_Text RoomID;
    public TMP_Text Username;
    public TMP_Text NbOfPlayers;
    public TMP_Text UsernameDisplay;

    void Start()
    {
        if (photonView.IsMine)
        {
            // C'est mon joueur → activer la caméra
            Cinemachine.SetActive(true);
            UsernameDisplay.gameObject.SetActive(false);
            gameObject.tag = "Player"; // Pour les collisions avec la caméra
        }

        RoomID.text = "Room ID : " + PhotonNetwork.CurrentRoom.Name;
        Username.text = "Username : " + PhotonNetwork.NickName;
        UsernameDisplay.text = PhotonNetwork.NickName;
    }

    void Update()
    {
        NbOfPlayers.text = "Nb of players : " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }
}
