using System;
using System.Collections;
using System.Collections.Generic;
using Animation;
using DG.Tweening;
using UnityEngine;
using Company.StateMachine;
using FX;
using Health;
using Interfaces;

namespace Enemy.Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }
    
    public class BossBase : MonoBehaviour, IDamageable
    {
        [Header("Animation")] 
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;
        [SerializeField] private AnimationBase _animationBase;

        [Header("Attack")] 
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;
        public float attackEndScale = 1.1f;
        public float attackScaleDuration = .1f;
        
        [Header("Movements")]
        public float speed = 5f;
        public List<Transform> waypoints;

        [Header("Damage")]
        public HealthBase healthBase;
        public Collider collider;
        public FlashColor flashColor;
        public ParticleSystem particleSystem;

        private StateMachine<BossAction> _stateMachine;

        #region Unity Events
        private void Awake()
        {
            Init();
            healthBase.OnKill += OnBossKill;
        }

        #endregion

        #region Helpers Methods
        protected virtual void Init()
        {
            _stateMachine = new StateMachine<BossAction>();
            _stateMachine.Init();
            
            _stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            _stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            _stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            _stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());

            SwitchAttack();
        }

        private void OnBossKill(HealthBase h)
        {
            SwitchState(BossAction.DEATH);
        }
        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
        
        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }
        #endregion

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback)
        {
            int attacks = 0;
            while (attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(attackEndScale, attackScaleDuration).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            endCallback?.Invoke();
        }
        #endregion

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }
        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            // filling the params dynamically, like the * on Ruby because of the params C# feature
            _stateMachine.SwitchState(state, false, this); // false, this, gameObject, transform); 
        }
        #endregion

        #region IDamageable implementations
        public void Damage(float damage)
        {
            Debug.Log("Damage");
            OnDamage(damage);
        }
        
        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            // create impact effect based on the given dir of the damage
            transform.DOMove(transform.position - dir, .1f);
        }
        #endregion

        #region Enemy Events
        protected virtual void OnKill()
        {
            if (collider != null) collider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float damage)
        {
            if (flashColor != null) flashColor.Flash();
            if (particleSystem != null) particleSystem.Emit(15);
            
            healthBase.Damage(damage);
            if (healthBase.CurrentLife <= 0) OnKill();
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }
        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }
        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }
        #endregion
    }
}
