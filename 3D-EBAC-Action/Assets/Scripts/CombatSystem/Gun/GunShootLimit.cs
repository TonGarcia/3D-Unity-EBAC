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
        public List<UIUpdater> uiUpdaters;
        
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
                uiUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
                
                // it waits the end of the frame because it can be really fast and do not be visible to the player
                yield return new WaitForEndOfFrame();
            }

            _currentShoots = 0;
            _recharging = false;
        }
        
        private void UpdateUI()
        {
            uiUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
        }

        private void GetAllUIs()
        {
            //uiUpdaters = GameObject.FindObjectsOfType<UIUpdater>().ToList();
            uiUpdaters ??= new List<UIUpdater>();
            GameObject[] ammoObjects = GameObject.FindGameObjectsWithTag("GunAmmo");

            foreach (GameObject ammoObject in ammoObjects)
            {
                UIUpdater uiUpdater = ammoObject.GetComponent<UIUpdater>();
                if (uiUpdater != null) uiUpdaters.Add(uiUpdater);
                else Debug.LogWarning("GameObject with tag 'GunAmmo' is missing the UIUpdater component: " + ammoObject.name);
            }
        }
    }
}
