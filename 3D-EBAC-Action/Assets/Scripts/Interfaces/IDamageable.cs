using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        void Damage(float damage);
        void Damage(float damage, Vector3 dir);
    }
    
}
