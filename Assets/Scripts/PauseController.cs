using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviourPunCallbacks
{
    [Header("--- REFERENCES ---")]
    public PlayerStateController StateController;
    public GameObject Main;
    public GameObject Options;

    public void ResumeGame()
    {
        StateController.ChangeState(PlayerStateController.State.Playing);
    }

    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void ToOptions()
    {
        Main.SetActive(false);
        Options.SetActive(true);
    }

    public void ToMain()
    {
        Main.SetActive(true);
        Options.SetActive(false);
    }
}
