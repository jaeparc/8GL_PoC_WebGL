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
    }

    public void UpdateState(){
        if(Input.GetKeyUp(KeyCode.Escape))
            CC.ChangeState(PlayerStateController.State.Playing);
    }

    public void OnExit()
    {
        CC.PauseMenu.SetActive(false);
    }
}