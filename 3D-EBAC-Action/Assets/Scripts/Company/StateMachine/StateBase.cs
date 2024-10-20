using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Company.StateMachine
{
    public class StateBase
    {
        public virtual void OnStateEnter(params object[] objs)
        {
            // Debug.Log("OnStateEnter");
        }

        public virtual void OnStateStay()
        {
            // Debug.Log("OnStateStay");
        }

        public virtual void OnStateExit()
        {
            // Debug.Log("OnStateExit");
        }
    
    }

    public class StateRunning : StateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
        }
    
        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }

    public class StateDead : StateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
        }
    }
}
