using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Interactable : MonoBehaviourPun
{
    [Header("--- REFERENCES ---")]
    public TextMeshProUGUI InteractionText;
    public GameObject UseButton;

    private InputTypeDetector ITD;
    private Buzz _selectedBuzz;
    // Start is called before the first frame update
    void Start()
    {
        ITD = GetComponent<InputTypeDetector>();
        UseButton.SetActive(false);
        InteractionText.gameObject.SetActive(false);
    }

    void Update(){
        if(_selectedBuzz != null && Input.GetKeyDown(KeyCode.E))
        {
            UseBuzzer();
        }
    }

    [PunRPC]
    public void UseBuzzer()
    {
        if (_selectedBuzz != null)
        {
            photonView.RPC("HasBuzzed", RpcTarget.All, _selectedBuzz.IDcampus);
            if (UseButton.activeSelf)
                UseButton.SetActive(false);
            else if (InteractionText.gameObject.activeSelf)
                InteractionText.gameObject.SetActive(false);
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
        if (ITD.lastInput == InputTypeDetector.LastInputType.Mouse)
            InteractionText.gameObject.SetActive(true);
        else
            UseButton.SetActive(true);
    }

    public void StopBuzz()
    {
        _selectedBuzz = null;
        InteractionText.gameObject.SetActive(false);
        UseButton.SetActive(false);
    }
}
