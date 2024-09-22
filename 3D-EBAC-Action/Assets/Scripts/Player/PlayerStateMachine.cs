using System;
using Company.Core;
using Company.StateMachine;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : Singleton<PlayerStateMachine>
    {
        public StateMachine<PlayerStates> stateMachine;
        private Animator _animator;
        public enum PlayerStates
        {
            IDLE,
            JUMP,
            RUN
        }

        #region Unity Events
        // Start is called before the first frame update
        private void Start()
        {
            Init();
        }
        
        // Update is called once per frame
        public void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                stateMachine.SwitchState(PlayerStates.RUN);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                stateMachine.SwitchState(PlayerStates.IDLE);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.SwitchState(PlayerStates.JUMP);
            }
        }
        #endregion
        
        public void Init()
        {
            _animator = GetComponent<Animator>();
            stateMachine = new StateMachine<PlayerStates>(_animator);
            stateMachine.Init();
            stateMachine.RegisterAllStates("Player", Enum.GetValues(typeof(PlayerStates)), "State");
            stateMachine.SwitchState(PlayerStates.IDLE);
        }
        
    }    
}
