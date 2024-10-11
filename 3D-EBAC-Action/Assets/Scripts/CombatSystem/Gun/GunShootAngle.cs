using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem.Gun
{
    public class GunShootAngle : GunShootLimit
    {
        public int amountPerShoot = 4;
        public float angle = 15f;

        protected override void Shoot()
        {
            int multipler = 0;
            
            for (int i = 0; i < amountPerShoot; i++)
            {
                if (i % 2 == 0)
                {
                    multipler++;
                }
                
                // using shootSpawn to force 0/reset position
                var projectile = Instantiate(prefabProjectile, shootSpawn);
                projectile.transform.localPosition = Vector3.zero;
                
                // Vector3.up means (0,1,0)
                // if odd number it switch the angle to avoid bullets on some location
                projectile.transform.localEulerAngles = Vector3.zero + Vector3.up * (i % 2 == 0 ? angle : -angle) * multipler;
                
                projectile.speed = speed;
                projectile.transform.parent = null;
            }
            
            
        }
    }
}
