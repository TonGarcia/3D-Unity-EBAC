using Animation;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float startLife = 10f;
        [SerializeField] private float _currentLife;

        [Header("Animation Transition")]
        [SerializeField] private AnimationBase _animationBase;
        
        [Header("Spawn Animation")] 
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        #region Unity Events
        private void Awake()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            // Debug
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }
        #endregion

        #region Enemy Helpers
        protected virtual void Init()
        {
            ResetLife();
            if (startWithBornAnimation) BornAnimation();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }
        
        protected virtual void Kill()
        {
            OnKill();
        }
        #endregion

        #region Enemy Events
        protected virtual void OnKill()
        {
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float damage)
        {
            _currentLife -= damage;
            if (_currentLife <= 0)
            {
                Kill();
            }
        }
        #endregion

        #region Animation
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }
        #endregion
    }
}
