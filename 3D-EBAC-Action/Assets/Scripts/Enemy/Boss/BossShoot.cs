using CombatSystem.Gun;

namespace Enemy.Boss
{
    public class BossShoot : BossBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();
            gunBase.StartShoot();
        }
    }
}
