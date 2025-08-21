using Photon.Pun;
using UnityEngine;

public class PlayerSetup : MonoBehaviourPun
{
    public GameObject Cinemachine;

    void Start()
    {
        if (photonView.IsMine)
        {
            // C'est mon joueur → activer la caméra
            Cinemachine.SetActive(true);
        }
    }
}
