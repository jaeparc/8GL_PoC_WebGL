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
        CC.PInputs.ActivateInput();
        if (CC.GetComponent<InputTypeDetector>().lastInput == InputTypeDetector.LastInputType.Mouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void UpdateState()
    {
        if (Input.GetKeyUp(KeyCode.T))
            CC.ChangeState(PlayerStateController.State.Chatting);
        else if(Input.GetKeyUp(KeyCode.P))
            CC.ChangeState(PlayerStateController.State.Pause);
    }

    public void OnExit()
    {
        CC.PInputs.DeactivateInput();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}