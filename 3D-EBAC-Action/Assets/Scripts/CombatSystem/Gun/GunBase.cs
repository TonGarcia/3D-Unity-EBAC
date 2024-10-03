using System.Collections;
using UnityEngine;
using Utils;

namespace CombatSystem.Gun
{
    public class GunBase : MonoBehaviour
    {
        public ProjectileBase prefabProjectile;
        public Transform shootSpawn;
        public float coolDownShoots = 0.3f;
        public Transform playerDirectionReference;
        private Coroutine _currentCoroutine;

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

        IEnumerator StartShooting()
        {
            while (true)
            {
                Shoot();
                yield return new WaitForSeconds(coolDownShoots);
            }
        }

        private void Shoot()
        {
            var projectile = Instantiate(prefabProjectile);
            projectile.transform.position = shootSpawn.position;
            projectile.transform.rotation = shootSpawn.rotation;
        }

        private void OnDestroy()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }
    }
}