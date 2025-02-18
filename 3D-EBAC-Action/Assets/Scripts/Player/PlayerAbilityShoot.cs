using System.Collections.Generic;
using CombatSystem.Gun;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class PlayerAbilityShoot : PlayerAbilityBase
    {
        public GunBase gunBase;
        public Transform gunPosition;
        public List<GunBase> inventory;
        public List<UIUpdater> UIUpdaters;
        
        private GunBase _currentGun;

        protected override void Init()
        {
            base.Init();
            GetAllUIs();
            CreateGun();

            Inputs.GamePlay.Shoot.performed += ctx => StartShoot();
            Inputs.GamePlay.Shoot.canceled += ctx => CancelShoot();
            
            Inputs.GamePlay.Gun1.performed += ctx => StartSwitchGun(0);
            Inputs.GamePlay.Gun1.canceled += ctx => CancelSwitchGun(0);
            Inputs.GamePlay.Gun2.performed += ctx => StartSwitchGun(1);
            Inputs.GamePlay.Gun2.canceled += ctx => CancelSwitchGun(1);
        }

        private void CreateGun()
        {
            // get positioned instantiated gun
            _currentGun = Instantiate(gunBase, gunPosition);
            _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        }

        private void StartSwitchGun(int gunIndex)
        {
            gunBase = inventory[gunIndex];
            CreateGun();
        }

        private void CancelSwitchGun(int gunIndex)
        {
            UIUpdaters.ForEach(i => i.UpdateValue(1));
        }
        
        private void StartShoot()
        {
            _currentGun.StartShoot();
            Debug.Log("Start Shoot");
        }
        
        private void CancelShoot()
        {
            _currentGun.StopShoot();
            Debug.Log("Cancel Shoot");
        }
        
        private void GetAllUIs()
        {
            UIUpdaters = GameObject.FindObjectsOfType<UIUpdater>().ToList();
        }
    }    
}
