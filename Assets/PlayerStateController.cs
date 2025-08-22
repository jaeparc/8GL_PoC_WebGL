using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    [HideInInspector]
    public enum State
    {
        Playing, Chatting, Pause
    }

    [HideInInspector]
    public Dictionary<State, IPlayerState> PlayerStatesDic = new();

    [Header("--- REFERENCES ---")]
    public StarterAssetsInputs SPInputs;
    public ThirdPersonController TPController;
    public ChatManagerRPC ChatManager;
    public GameObject PauseMenu;

    private IPlayerState _currentState;


    // Start is called before the first frame update
    void Start()
    {
        _initStates();
        ChangeState(State.Playing);
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentState != null)
            _currentState.UpdateState();
    }
    
    private void _initStates(){
        PlayerStatesDic.Add(State.Playing,new PlayingState(this));
        PlayerStatesDic.Add(State.Chatting,new ChattingState(this));
        PlayerStatesDic.Add(State.Pause,new PauseState(this));

        ChangeState(State.Playing);
    }

    public void ChangeState(State newState){
        if(_currentState != null)
            _currentState.OnExit();
        _currentState = PlayerStatesDic[newState];
        _currentState.OnEnter();
    }

    public interface IPlayerState
    {
        public PlayerStateController CC { get; }
        public void OnEnter();
        public void UpdateState();
        public void OnExit();
    }
}
