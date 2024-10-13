using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float startLife = 10f;
        [SerializeField] private float _currentLife;

        #region Unity Events
        private void Awake()
        {
            Init();
        }

        #endregion
        
        // Start is called before the first frame update
        void Start()
        {
        
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

        #region Enemy Helpers
        protected virtual void Init()
        {
            ResetLife();
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
            Destroy(gameObject);
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
    }
}
