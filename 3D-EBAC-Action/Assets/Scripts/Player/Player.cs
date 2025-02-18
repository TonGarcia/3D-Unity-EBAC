using System.Collections.Generic;
using Animation;
using DG.Tweening;
using FX;
using Health;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        #region Unity Reference Attributes
        public CharacterController characterController;
        public Animator animator;
        public KeyCode jumpKeyCode = KeyCode.Space;
        public KeyCode runKeyCode = KeyCode.LeftShift;
        #endregion

        #region Speed Modifier Attributes
        public float speed = 1f; // movements speed sensitive control
        public float turnSpeed = 1f; // targeting sensitive control (if greater so faster, if lower it means slower)
        public float gravity = 9.8f;
        private float _vSpeed = 0f;
        private static readonly int Run = Animator.StringToHash("Run");
        public float jumpForceSpeed = 15f;
        public float runSpeed = 1.5f;
        #endregion

        #region Damage
        [Header("Health")]
        public List<FlashColor> flashColors;
        public HealthBase healthBase;
        public ParticleSystem particleSystem;
        public Collider collider;
        [SerializeField] private AnimationBase _animationBase;
        private bool _isDead = false;
        #endregion
        
        #region Unity Events
        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void Awake()
        {
            OnValidate();
            healthBase.OnDamage += Damage;
            healthBase.OnDamage += OnKill;
        }

        private void OnValidate()
        {
            if(healthBase == null) healthBase = GetComponent<HealthBase>();
        }

        // Update is called once per frame
        void Update()
        {
            // how much the player is trying to move left or right (horizontal) -1 to 1
            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
            // how much the player is trying to move left or right (vertical) -1 to 1
            var inputAxisVertical = Input.GetAxis("Vertical");

            // calculating speed vector to next point to be moved to based on speed & "move force based on InputAxis" 
            var speedVector = transform.forward * inputAxisVertical * speed;

            if(characterController.isGrounded)
            {
                _vSpeed = 0;
                if(Input.GetKeyDown(jumpKeyCode))
                {
                    _vSpeed = jumpForceSpeed;
                }
            }

            // calculating the speed considering the time elapsed (not by frame) & GRAVITY force
            _vSpeed -= gravity * Time.deltaTime;
            speedVector.y = _vSpeed;

            var isWalking = inputAxisVertical != 0;
            if(isWalking)
            {
                // GetKey = pressing ; GetKeyDown = when start a click (not track pressing) ; GetKeyUp = unpressed
                if(Input.GetKey(runKeyCode))
                {
                    speedVector *= runSpeed; // update the GameObject move speed
                    animator.speed = runSpeed; // update the Animator animation execution speed
                }
                else
                {
                    animator.speed = 1;
                }
            }
            
            characterController.Move(speedVector * Time.deltaTime);
            
            // activate the animations by changing the Parameter values
            animator.SetBool("Run", inputAxisVertical != 0);
        }
        #endregion

        #region IDamageable implementations
        public void Damage(float damage)
        {
            flashColors.ForEach(i => i.Flash());
            OnDamage(damage);
        }

        public void Damage(HealthBase health)
        {
            flashColors.ForEach(i => i.Flash());
        }

        public void Damage(float damage, Vector3 dir)
        {
            // create impact effect based on the given dir of the damage
            transform.DOMove(transform.position - dir, .1f);
            Damage(damage);
        }
        #endregion

        #region PlayerEvents
        public void OnDamage(float damage)
        {
            flashColors.ForEach(i => i.Flash());
            if (particleSystem != null) particleSystem.Emit(15);

            healthBase.Damage(damage);
            if (healthBase.CurrentLife <= 0) OnKill(healthBase);
        }

        protected virtual void OnKill(HealthBase health)
        {
            if (healthBase.CurrentLife > 0) return;
            if (_isDead) return;
            if (collider != null) collider.enabled = false;
            if (health.destroyOnKill) Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
            _isDead = true;
        }
        #endregion

        #region ANIMATIONS
        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }
        #endregion
    }
}
