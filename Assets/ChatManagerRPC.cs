using System.Collections;
using Photon.Pun;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManagerRPC : MonoBehaviourPun
{
    [Header("--- REFERENCES ---")]
    public GameObject ChatPanel; // Panneau de chat
    public TMP_InputField TextInput;  // Champ pour taper le message
    public TMP_Text Display;      // Affichage des messages
    public PlayerStateController StateController;

    [Header("--- SETTINGS ---")]
    public float ChatDuration;

    private bool _editing = false;
    private float _timer;

    void Start()
    {
        Display.text = "";
        TextInput.onSubmit.AddListener(Submit);
        TextInput.onSelect.AddListener(Focus);
        TextInput.onDeselect.AddListener(LostFocus);
    }

    void Update()
    {
        Chrono();
    }

    void Chrono()
    {
        if(!_editing && _timer < ChatDuration)
        {
            _timer += Time.deltaTime;
        }
        else if(_timer >= ChatDuration)
        {
            ChatPanel.SetActive(false);
            _timer = 0;
        }
    }

    public void OpenChat()
    {
        _editing = true;
        _timer = 0;
        TextInput.ActivateInputField();
    }

    void Focus(string text)
    {
        _editing = true;
        _timer = 0;
        StateController.ChangeState(PlayerStateController.State.Chatting);
    }

    void LostFocus(string text)
    {
        _editing = false;
        _timer = 0;
        StateController.ChangeState(PlayerStateController.State.Playing);
    }


    [PunRPC]
    void Submit(string message)
    {
        _editing = false;
        _timer = 0;
        if (!string.IsNullOrEmpty(message))
        {
            // Envoie le message à tous via RPC
            photonView.RPC("ReceiveMessage", RpcTarget.All, PhotonNetwork.NickName, message);
            TextInput.text = ""; // vide le champ
        }
        StateController.ChangeState(PlayerStateController.State.Playing);
    }

    [PunRPC]
    void ReceiveMessage(string senderName, string message)
    {
        _editing = false;
        _timer = 0;
        ChatPanel.SetActive(true);
        Display.text += $"{senderName}: {message}\n";
    }
}
