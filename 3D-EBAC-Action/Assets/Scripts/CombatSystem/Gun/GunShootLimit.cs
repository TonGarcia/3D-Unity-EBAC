using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CombatSystem.Gun
{
    public class GunShootLimit : GunBase
    {
        public List<UIUpdater> UIUpdaters;
        
        public float maxShoot = 5;
        public float timeToRecharge = 1f;
        
        private float _currentShoots;
        private bool _recharging = false;

        private void Awake()
        {
            GetAllUIs();
        }

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
                    UpdateUI();
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
                UIUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
                
                // it waits the end of the frame because it can be really fast and do not be visible to the player
                yield return new WaitForEndOfFrame();
            }

            _currentShoots = 0;
            _recharging = false;
        }
        
        private void UpdateUI()
        {
            UIUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
        }

        private void GetAllUIs()
        {
            UIUpdaters = GameObject.FindObjectsOfType<UIUpdater>().ToList();
        }
    }
}
