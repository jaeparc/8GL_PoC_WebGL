using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform spawnPoint;

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Playground")
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        if (spawnPoint != null)
        {
            Debug.Log("Spawning player in Playground...");
            PhotonNetwork.Instantiate("PlayerArmature", spawnPoint.position, spawnPoint.rotation);
        }
    }
}