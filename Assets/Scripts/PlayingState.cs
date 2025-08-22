using System.Threading;
using UnityEngine;

public class PlayingState : PlayerStateController.IPlayerState
{
    public PlayerStateController CC { get; private set; }
    public PlayingState(PlayerStateController controller){
        CC = controller;
    }
    public void OnEnter()
    {
        CC.TPController.enabled = true;
        CC.SPInputs.cursorInputForLook = true;
        CC.SPInputs.cursorLocked = true;
    }

    public void UpdateState()
    {
        if (Input.GetKeyUp(KeyCode.T))
            CC.ChangeState(PlayerStateController.State.Chatting);
        else if(Input.GetKeyUp(KeyCode.P))
            CC.ChangeState(PlayerStateController.State.Pause);
    }

    public void OnExit(){
        CC.TPController.enabled = false;
        CC.SPInputs.cursorInputForLook = false;
        CC.SPInputs.cursorLocked = false;
    }
}