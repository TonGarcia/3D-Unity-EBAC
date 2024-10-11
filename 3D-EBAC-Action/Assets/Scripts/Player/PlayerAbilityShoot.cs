using System;
using CombatSystem.Gun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerAbilityShoot : PlayerAbilityBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();
            Inputs.GamePlay.Shoot.performed += ctx => StartShoot();
            Inputs.GamePlay.Shoot.canceled += ctx => CancelShoot();
        }
        
        private void StartShoot()
        {
            gunBase.StartShoot();
            Debug.Log("Start Shoot");
        }
        
        private void CancelShoot()
        {
            gunBase.StopShoot();
            Debug.Log("Cancel Shoot");
        }
    }    
}
