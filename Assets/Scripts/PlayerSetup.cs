using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviourPun
{
    public GameObject Cinemachine;
    public GameObject Canvas;
    public TMP_Text RoomID;
    public TMP_Text Username;
    public TMP_Text NbOfPlayers;
    public TMP_Text UsernameDisplay;
    public PlayerInput Inputs;

    [HideInInspector] public GameObject LocalPlayerInstance;

    void Start()
    {
        if (photonView.IsMine)
        {
            // C'est mon joueur → activer la caméra
            Cinemachine.SetActive(true);
            UsernameDisplay.gameObject.SetActive(false);
            gameObject.tag = "Player"; // Pour les collisions avec la caméra
            RoomID.text = "Room ID : " + PhotonNetwork.CurrentRoom.Name;
            Username.text = "Username : " + PhotonNetwork.NickName;
            Canvas.SetActive(true);
            Inputs.enabled = true;
            GetComponent<PlayerStateController>().enabled = true;
        }
        else
        {
            // C'est le joueur d'un autre → désactiver la caméra
            Cinemachine.SetActive(false);
            UsernameDisplay.gameObject.SetActive(true);
            UsernameDisplay.text = photonView.Owner.NickName;
            Canvas.SetActive(false);
            Inputs.enabled = false;
            GetComponent<PlayerStateController>().enabled = false;
        }
        GameObject.FindWithTag("GameManager").GetComponent<NewGameManager>().PlayersInGame.Add(gameObject);
    }

    void Update()
    {
        NbOfPlayers.text = "Nb of players : " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        if(!photonView.IsMine && LocalPlayerInstance != null)
        {
            UsernameDisplay.transform.LookAt(LocalPlayerInstance.GetComponentInChildren<Camera>().transform);
        }
    }
}
