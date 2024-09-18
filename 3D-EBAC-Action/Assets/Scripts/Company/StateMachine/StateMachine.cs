using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using NaughtyAttributes;

public class StateMachine<T> where T : System.Enum
{

    public Dictionary<T, StateBase> dictionaryState;

    private StateBase _currentState;
    public float timeToStartGame = 1f;

    public StateBase CurrentState
    {
        get { return _currentState; }
    }
    
    public void Init()
    {
        dictionaryState ??= new Dictionary<T, StateBase>();
    }
    
    public void RegisterStates(T enumType, StateBase state)
    {
        dictionaryState.Add(enumType, state);
    }

    public void SwitchState(T state)
    {
        if(_currentState != null) _currentState.OnStateExit();
        _currentState = dictionaryState[state];
        _currentState.OnStateEnter();
    }

    // Update is called once per frame
    public void Update()
    {
        if(_currentState != null) _currentState.OnStateStay();
        
    }

#if UNITY_EDITOR
    #region DEBUG

    [Button]
    private void ChangeStateToX()
    {
        // SwitchState(States.NONE);
    }
    
    [Button]
    private void ChangeStateToY()
    {
        // SwitchState(States.NONE);
    }
    
    #endregion
#endif

}
