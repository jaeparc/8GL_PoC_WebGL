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
    public ScrollRect SR;
    public InputField TextInput;  // Champ pour taper le message
    public TMP_Text Display;      // Affichage des messages
    public PlayerStateController StateController;

    [Header("--- SETTINGS ---")]
    public float ChatDuration;

    private bool _editing = false;
    private float _timer;
    private bool _wasFocused = false;

    void Start()
    {
        Display.text = "";
        TextInput.onEndEdit.AddListener(Submit); // au lieu de onSubmit
    }

    void Update()
    {
        Chrono();
        HandleFocusTransitions();
    }

    void HandleFocusTransitions()
    {
        bool focused = TextInput != null && TextInput.isFocused;

        if (focused && !_wasFocused)
        {
            Focus();
        }
        else if (!focused && _wasFocused)
        {
            LostFocus();
        }

        _wasFocused = focused;
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

    void Focus()
    {
        _editing = true;
        _timer = 0;
        StateController.ChangeState(PlayerStateController.State.Chatting);
    }

    void LostFocus()
    {
        _editing = false;
        _timer = 0;
        StateController.ChangeState(PlayerStateController.State.Playing);
    }

    [PunRPC]
    public void Submit(string message)
    {
        _editing = false;
        _timer = 0;
        if (!string.IsNullOrEmpty(message))
        {
            // Envoie le message à tous via RPC
            foreach (var obj in FindObjectsOfType<ChatManagerRPC>())
            {
                obj.photonView.RPC("ReceiveMessage", RpcTarget.All, PhotonNetwork.NickName, message);
            }
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
        Display.text += $"{senderName} : {message}\n";
        Canvas.ForceUpdateCanvases();
        SR.verticalNormalizedPosition = 0f;
    }
}
