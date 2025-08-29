using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Interactable : MonoBehaviourPun
{
    [Header("--- REFERENCES ---")]
    public TextMeshProUGUI InteractionText;

    private Buzz _selectedBuzz;
    // Start is called before the first frame update
    void Start()
    {
        InteractionText.gameObject.SetActive(false);
    }

    void Update(){
        if(_selectedBuzz != null && Input.GetKeyDown(KeyCode.E))
        {
            UseBuzzer();
        }
    }

    [PunRPC]
    void UseBuzzer()
    {
        if (_selectedBuzz != null)
        {
            photonView.RPC("HasBuzzed", RpcTarget.All, _selectedBuzz.IDcampus);
        }
    }

    [PunRPC]
    void HasBuzzed(string campusID)
    {
        GetComponent<PlayerSetup>().GM.Displayer.DisplayCampusInfo(campusID);
    }

    public void CanBuzz(Buzz buzzer)
    {
        _selectedBuzz = buzzer;
        InteractionText.gameObject.SetActive(true);
    }

    public void StopBuzz()
    {
        _selectedBuzz = null;
        InteractionText.gameObject.SetActive(false);
    }
}
