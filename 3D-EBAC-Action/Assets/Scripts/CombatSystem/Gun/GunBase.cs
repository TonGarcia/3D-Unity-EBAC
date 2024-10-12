using System.Collections;
using UnityEngine;

namespace CombatSystem.Gun
{
    public class GunBase : MonoBehaviour
    {
        public ProjectileBase prefabProjectile;
        public Transform shootSpawn;
        public float coolDownShoots = 0.3f;
        public Transform playerDirectionReference;
        public float speed = 50f;
        private Coroutine _currentCoroutine;

        #region UnityEvents
        private void OnDestroy()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }
        
        /*
         * This Update was replaaced by the NEW INPUT SYSTEM events instead of each frame validations
        private void Update()
           {
               if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
               {
                   if (_currentCoroutine != null)
                   {
                       StopCoroutine(_currentCoroutine);
                   }
                   _currentCoroutine = StartCoroutine(StartShooting());
               }
               else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
               {
                   if (_currentCoroutine != null)
                   {
                       StopCoroutine(_currentCoroutine);
                       _currentCoroutine = null;
                   }
               }
           }
         */
        #endregion

        protected virtual IEnumerator StartShooting()
        {
            while (true)
            {
                Shoot();
                yield return new WaitForSeconds(coolDownShoots);
            }
        }

        protected virtual void Shoot()
        {
            var projectile = Instantiate(prefabProjectile);
            projectile.transform.position = shootSpawn.position;
            projectile.transform.rotation = shootSpawn.rotation;
            projectile.speed = speed;
        }

        public void StartShoot()
        {
            StopShoot();
            _currentCoroutine = StartCoroutine(StartShooting());
        }

        public void StopShoot()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }

        
    }
}