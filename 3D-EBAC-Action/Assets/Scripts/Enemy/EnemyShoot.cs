using CombatSystem.Gun;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();
            gunBase.StartShoot();
        }
    }
}
