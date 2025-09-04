using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using Photon.Realtime;
using StarterAssets;

public class PlayerSetup : MonoBehaviourPun
{
    [Header("--- CAMERAS ---")]
    public CinemachineVirtualCamera CM;
    public Camera Cam;
    [Header("--- UI ---")]
    public GameObject Canvas;
    public TMP_Text RoomID;
    public TMP_Text Username;
    public TMP_Text NbOfPlayers;
    [Header("--- WORLD SPACE UI ---")]
    public TMP_Text UsernameDisplay;
    [Header("--- PLAYER ---")]
    public PlayerInput Inputs;
    public GameObject Geometry;
    public GameObject Skeleton;
    public ThirdPersonController TPController;

    [HideInInspector] public GameObject LocalPlayerInstance;
    [HideInInspector] public NewGameManager GM;

    void Start()
    {
        if (photonView.IsMine)
        {
            CM.gameObject.SetActive(true);
            Cam.gameObject.SetActive(true);
            UsernameDisplay.gameObject.SetActive(false);
            gameObject.tag = "Player"; // Pour les collisions avec la caméra
            RoomID.text = "Room ID : " + PhotonNetwork.CurrentRoom.Name;
            Username.text = "Username : " + PhotonNetwork.NickName;
            Canvas.SetActive(true);
            Inputs.enabled = true;
            GetComponent<PlayerStateController>().enabled = true;

            Geometry.SetActive(false);
            Skeleton.SetActive(false);
        }
        else
        {
            CM.gameObject.SetActive(false);
            Cam.gameObject.SetActive(false);
            UsernameDisplay.gameObject.SetActive(true);
            UsernameDisplay.text = photonView.Owner.NickName;
            Canvas.SetActive(false);
            Inputs.enabled = false;
            GetComponent<PlayerStateController>().enabled = false;
        }
        GM = GameObject.FindWithTag("GameManager").GetComponent<NewGameManager>();
        GM.PlayersInGame.Add(gameObject);
        LocalPlayerInstance = GM.MainPlayer;
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
