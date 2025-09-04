using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;
        public PlayerStateController playerStateController;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            if (Mathf.Abs(virtualMoveDirection.x) > .7f || Mathf.Abs(virtualMoveDirection.y) > .7f)
                VirtualSprintInput(true);
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs.SprintInput(virtualSprintState);
        }

        public void VirtualPauseInput()
        {
            playerStateController.ChangeState(PlayerStateController.State.Pause);
        }

        public void VirtualChatInput()
        {
            playerStateController.ChangeState(PlayerStateController.State.Chatting);
        }
    }

}
