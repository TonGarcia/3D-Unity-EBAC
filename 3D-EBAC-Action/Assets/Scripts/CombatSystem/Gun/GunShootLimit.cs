using System.Collections;
using UnityEngine;

namespace CombatSystem.Gun
{
    public class GunShootLimit : GunBase
    {
        public float maxShoot = 5;
        public float timeToRecharge = 1f;
        
        private float _currentShoots;
        private bool _recharging = false;

        protected override IEnumerator StartShooting()
        {
            if(_recharging) yield break; // means return on a coroutine
            
            // TODO: avoid while true
            while (true)
            {
                if (_currentShoots < maxShoot)
                {
                    Shoot();
                    _currentShoots++;
                    CheckRecharge();
                    yield return new WaitForSeconds(timeToRecharge);
                }
            }
        }


        private void CheckRecharge()
        {
            if (_currentShoots >= maxShoot)
            {
                StopShoot();
                StartRecharge();
            }
        }

        private void StartRecharge()
        {
            _recharging = true;
            StartCoroutine(RechargeCoroutine());
        }

        IEnumerator RechargeCoroutine()
        {
            float time = 0;
            while (time < timeToRecharge)
            {
                // time get the time running on screen to wait time reloading (fake waiting) 
                time += Time.deltaTime;
                Debug.Log("Recharging: " + time);
                
                // it waits the end of the frame because it can be really fast and do not be visible to the player
                yield return new WaitForEndOfFrame();
            }

            _currentShoots = 0;
            _recharging = false;
        }
    }
}
