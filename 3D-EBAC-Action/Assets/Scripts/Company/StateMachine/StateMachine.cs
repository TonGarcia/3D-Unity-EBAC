using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using NaughtyAttributes;

public class StateMachine : MonoBehaviour
{

    public enum States
    {
        NONE,
        // IDLE,
        // RUNNING,
        // DEAD
    }

    public Dictionary<States, StateBase> dictionaryState;

    private StateBase _currentState;
    public float timeToStartGame = 1f;
    
    private void Awake()
    {
        dictionaryState = new Dictionary<States, StateBase>();
        // dictionaryState.Add(States.IDLE, new StateBase());
        // dictionaryState.Add(States.RUNNING, new StateRunning());
        // dictionaryState.Add(States.DEAD, new StateDead());
        
        //dictionaryState[States.DEAD]
        // SwitchState(States.IDLE);
        SwitchState(States.NONE);
        
        Invoke(nameof(StartGame), timeToStartGame);
    }

    [Button]
    private void StartGame()
    {
        // SwitchState(States.RUNNING);
        SwitchState(States.NONE);
    }

    private void SwitchState(States state)
    {
        if(_currentState != null) _currentState.OnStateExit();
        _currentState = dictionaryState[state];
        _currentState.OnStateEnter();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentState != null) _currentState.OnStateStay();
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // SwitchState(States.DEAD);
            SwitchState(States.NONE);
        }
    }

#if UNITY_EDITOR
    #region DEBUG

    [Button]
    private void ChangeStateToX()
    {
        SwitchState(States.NONE);
    }
    
    [Button]
    private void ChangeStateToY()
    {
        SwitchState(States.NONE);
    }
    
    #endregion
#endif

}
