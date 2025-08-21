using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManagerRPC : MonoBehaviourPun
{
    public TMP_InputField chatInput;  // Champ pour taper le message
    public TMP_Text chatDisplay;      // Affichage des messages
    public Button sendButton;

    void Start()
    {
        chatDisplay.text = "";
        sendButton.onClick.AddListener(OnSendButtonClicked);
    }

    void OnSendButtonClicked()
    {
        string message = chatInput.text;
        if (!string.IsNullOrEmpty(message))
        {
            // Envoie le message à tous via RPC
            photonView.RPC("ReceiveMessage", RpcTarget.All, PhotonNetwork.NickName, message);
            chatInput.text = ""; // vide le champ
        }
    }

    [PunRPC]
    void ReceiveMessage(string senderName, string message)
    {
        chatDisplay.text += $"{senderName}: {message}\n";
    }
}
