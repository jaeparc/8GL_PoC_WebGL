using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameManager : MonoBehaviourPunCallbacks
{
    [Header("--- REFERENCES ---")]
    public GameObject PrefabPlayer;
    public Transform SpawnPoint;
    public List<GameObject> PlayersInGame = new List<GameObject>();

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
        GameObject instance = PhotonNetwork.Instantiate(this.PrefabPlayer.name, SpawnPoint.position, SpawnPoint.rotation, 0);
        foreach (GameObject player in PlayersInGame)
        {
            player.GetComponent<PlayerSetup>().LocalPlayerInstance = instance;
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Log");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Log");
    }
}
