using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerAbilityBase : MonoBehaviour
    {
        protected Player Player;
        protected Inputs Inputs;

        #region Unity Events
        // Validate before the Start
        private void OnValidate()
        {
            if (Player == null) Player = GetComponent<Player>();
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            Inputs = new Inputs();
            Inputs.Enable();
            
            // Init before the validate to be sure that if the OnValidate did not work before Start it will next
            Init();
            
            // The OnValidate is called before the start, but sometimes it cause race condition issue
            OnValidate();
            
            // Sign all listeners
            RegisterListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void OnEnable()
        {
            if(Inputs != null) Inputs.Enable();
        }

        private void OnDisable()
        {
            Inputs.Disable();
        }
        #endregion

        #region Helpers to be Overrided
        // Init conditions
        protected virtual void Init() { }
        // Register events listeners
        protected virtual void RegisterListeners() { }
        // Remove events listeners to avoid references passed over scenes
        protected virtual void RemoveListeners() { }
        #endregion
    }
    
}
