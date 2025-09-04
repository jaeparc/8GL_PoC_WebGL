using System.Threading;
using UnityEngine;

public class PauseState : PlayerStateController.IPlayerState
{
    public PlayerStateController CC { get; private set; }
    public PauseState(PlayerStateController controller){
        CC = controller;
    }
    public void OnEnter()
    {
        CC.PauseMenu.SetActive(true);
        if(CC.GetComponent<InputTypeDetector>().lastInput == InputTypeDetector.LastInputType.Touch)
            CC.MobileUI.SetActive(false);
    }

    public void UpdateState(){
        if(Input.GetKeyUp(KeyCode.P))
            CC.ChangeState(PlayerStateController.State.Playing);
    }

    public void OnExit()
    {
        CC.PauseMenu.SetActive(false);
        if(CC.GetComponent<InputTypeDetector>().lastInput == InputTypeDetector.LastInputType.Touch)
            CC.MobileUI.SetActive(true);
    }
}