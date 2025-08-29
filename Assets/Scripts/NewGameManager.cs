using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameManager : MonoBehaviourPunCallbacks
{
    [Header("--- REFERENCES ---")]
    public Camera SpectatorCamera;
    public GameObject PrefabPlayer;
    public Transform SpawnPoint;
    public List<GameObject> PlayersInGame = new List<GameObject>();
    public GameObject MainPlayer;
    public CampusInfos Displayer;

    void Start()
    {
        if (PrefabPlayer == null)
        {
            Debug.LogError("Missing playerPrefab Reference", this);
        }
        else
        {
            StartCoroutine(SpawnPlayer());
        }
    }

    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(5f);
        Debug.LogFormat("Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
        MainPlayer = PhotonNetwork.Instantiate(this.PrefabPlayer.name, SpawnPoint.position, SpawnPoint.rotation, 0);
        SpectatorCamera.gameObject.SetActive(false);
        foreach (GameObject player in PlayersInGame)
        {
            player.GetComponent<PlayerSetup>().LocalPlayerInstance = MainPlayer;
        }
    }

    public override void OnLeftRoom()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Log");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Log");
    }
}
