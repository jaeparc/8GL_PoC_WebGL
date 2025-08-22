using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviourPunCallbacks
{
    [Header("--- REFERENCES ---")]
    public PlayerStateController StateController;

    public void ResumeGame()
    {
        StateController.ChangeState(PlayerStateController.State.Playing);
    }

    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Log"); // ou le nom de ta scène de connexion
    }
}
