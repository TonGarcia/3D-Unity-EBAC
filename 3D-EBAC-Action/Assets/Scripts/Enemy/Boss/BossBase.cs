using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Company.StateMachine;

namespace Enemy.Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK
    }
    
    public class BossBase : MonoBehaviour
    {
        [Header("Animation")] 
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;
        
        [Header("Config")]
        public float speed = 5f;
        public List<Transform> waypoints;
        
        private StateMachine<BossAction> _stateMachine;

        #region Unity Events

        private void Awake()
        {
            Init();
        }
        #endregion

        #region Helpers Methods
        private void Init()
        {
            _stateMachine = new StateMachine<BossAction>();
            _stateMachine.Init();
            
            _stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            _stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
        }
        #endregion

        #region Animation
        public void GoToRandomPoint()
        {
            StartCoroutine(GoToPointCoroutine(waypoints[Random.Range(0, waypoints.Count)]));
        }

        IEnumerator GoToPointCoroutine(Transform t)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
        }
        
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
        #endregion
        
        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            // filling the params dynamically, like the * on Ruby because of the params C# feature
            _stateMachine.SwitchState(state, false, this); // false, this, gameObject, transform); 
        }
        #endregion
    }
}
