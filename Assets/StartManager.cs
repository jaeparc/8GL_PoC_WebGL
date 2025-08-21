using Photon.Pun;
using UnityEngine;

public class StartManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform spawnPoint;

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom -> Spawning player...");
        PhotonNetwork.Instantiate("PlayerArmature", spawnPoint.position, spawnPoint.rotation);
    }
}
