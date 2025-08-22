using System.Threading;
using UnityEngine;

public class ChattingState : PlayerStateController.IPlayerState
{
    public PlayerStateController CC { get; private set; }
    public ChattingState(PlayerStateController controller){
        CC = controller;
    }
    public void OnEnter()
    {
        CC.ChatManager.ChatPanel.SetActive(true);
        CC.ChatManager.OpenChat();
    }

    public void UpdateState(){
    }

    public void OnExit(){
    }
}