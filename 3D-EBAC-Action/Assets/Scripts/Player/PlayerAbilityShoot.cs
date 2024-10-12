using CombatSystem.Gun;
using UnityEngine;

namespace Player
{
    public class PlayerAbilityShoot : PlayerAbilityBase
    {
        public GunBase gunBase;
        public Transform gunPosition;
        
        private GunBase _currentGun;

        protected override void Init()
        {
            base.Init();
            CreateGun();
            Inputs.GamePlay.Shoot.performed += ctx => StartShoot();
            Inputs.GamePlay.Shoot.canceled += ctx => CancelShoot();
        }

        private void CreateGun()
        {
            // get gun position to positionate the instantiated gun
            _currentGun = Instantiate(gunBase, gunPosition);
            _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
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
    }    
}
