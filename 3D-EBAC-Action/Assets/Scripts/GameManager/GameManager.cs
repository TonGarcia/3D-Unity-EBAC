using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Company.Core;
using Company.StateMachine;
using Utils;

public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        INTRO,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE
    }

    public StateMachine<GameStates> stateMachine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<GameStates>();
        stateMachine.Init();
        stateMachine.RegisterAllStates("GMState", Enum.GetValues(typeof(GameStates)));
        stateMachine.SwitchState(GameStates.INTRO);
    }
}
