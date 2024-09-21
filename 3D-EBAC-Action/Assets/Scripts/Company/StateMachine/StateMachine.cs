using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Company.StateMachine
{
     public class StateMachine<T> where T : System.Enum
     {
         #region Unity Attributes
         public Animator animatorReference;
         #endregion
         
         #region Setup Attributes
         public Dictionary<T, StateBase> dictionaryState;
         #endregion

         #region Logical Attributes
         private StateBase _currentState;
         public float timeToStartGame = 1f;
         #endregion
     
         public StateBase CurrentState
         {
             get { return _currentState; }
         }

         public StateMachine(Animator animator = null)
         {
             animatorReference = animator;
         }
         
         public void Init()
         {
             dictionaryState ??= new Dictionary<T, StateBase>();   
         }
         
         public void RegisterStates(T enumType, StateBase state)
         {
             dictionaryState.Add(enumType, state);
         }

         public void RegisterAllStates(string prefix, Array states, string sufix = "")
         {
             foreach (T state in states)
             {
                 string pascalCaseName = Helpers.ConvertSnakeCaseToPascalCase(state.ToString());
                 string className = $"{prefix}{pascalCaseName}{sufix}";
                 Type stateType = Type.GetType(className);
                 if (stateType != null && stateType.IsSubclassOf(typeof(StateBase)))
                 {
                     StateBase stateInstance = (StateBase)Activator.CreateInstance(stateType);
                     RegisterStates(state, stateInstance);
                 }
                 else
                 {
                     Debug.LogError($"State class '{className}' not found or not derived from StateBase.");
                 }
             }
         }
     
         public void SwitchState(T state, bool isBool = false)
         {
             // Add a null check before accessing the dictionary
             if (dictionaryState != null && dictionaryState.ContainsKey(state))
             {
                 _currentState = dictionaryState[state];
                 _currentState.OnStateEnter();
                 
                 // Trigger animation based on new state
                 TriggerAnimationForState(_currentState, isBool);
             }
             else
             {
                 Debug.LogError($"State '{state}' not found in the dictionary.");
             }
         }
         
         private void TriggerAnimationForState(StateBase state, bool isBool = false)
         { 
             if (!animatorReference.IsUnityNull())
             {
                 string trigger = state.GetType().Name;
                 
                 if (isBool)
                 {
                     animatorReference.SetBool(trigger, true);
                 }
                 else
                 {
                     // Trig the trigger
                     animatorReference.SetTrigger(trigger);
                     
                     // Logic to determine animation based on the state
                     // animatorReference.Play(state.GetType().Name); 

                     // Alternatively, using an enum:
                     // animator.SetInteger("State", (int)state.StateType);
                 }
             }
             else
             {
                 string msg = $"Animator component not found on the {typeof(T)}.";
                 Debug.LogWarning(msg);
             }
         }
     
         // Update is called once per frame
         public void Update()
         {
             if(_currentState != null) _currentState.OnStateStay();
             
         }
    }
}
