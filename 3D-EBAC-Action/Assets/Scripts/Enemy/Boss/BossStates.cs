using Company.StateMachine;
using UnityEngine;

namespace Enemy.Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void OnStateEnter(params object[] objs)
        {
            // on enter in any state type it set up the boss initial value
            base.OnStateEnter(objs);
            Debug.Log("Boss: " + boss);
            boss = (BossBase)objs[0];
        }
    }

    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            // on enter in any state type it set up the boss initial value
            base.OnStateEnter(objs);
            boss.StartInitAnimation();
        }
    }
    
    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            // on enter in any state type it set up the boss initial value
            base.OnStateEnter(objs);
            boss.GoToRandomPoint();
        }
    }
}
