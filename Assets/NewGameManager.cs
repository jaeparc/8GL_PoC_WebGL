using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NewGameManager : MonoBehaviourPunCallbacks
{
    [Header("--- REFERENCES ---")]
    public GameObject PrefabPlayer;
    public Transform SpawnPoint;

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
        // On attend 0.1s pour être sûr que Photon a synchronisé la scène
        yield return new WaitForSeconds(5f);
        Debug.LogFormat("Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
        PhotonNetwork.Instantiate(this.PrefabPlayer.name, SpawnPoint.position, SpawnPoint.rotation, 0);
    }
}
