using System;
using System.Collections.Generic;
using CombatSystem.Gun;
using Interfaces;
using UnityEngine;

namespace Health
{
    public class HealthBase : MonoBehaviour, IDamageable
    {
        public float startLife = 10f;
        public bool destroyOnKill = false;
        [SerializeField] private float _currentLife;
        public float CurrentLife => _currentLife; // Read-only property 
        public List<UIUpdater> uiUpdaters;

        public Action<HealthBase> OnKill;
        public Action<HealthBase> OnDamage;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            ResetLife();
        }
        
        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Kill()
        {
            if (destroyOnKill) Destroy(gameObject, 3f);
            OnKill?.Invoke(this);
        }

        [NaughtyAttributes.Button]
        public void Damage()
        {
            Damage(5f);
        }

        public void Damage(float damage)
        {
            _currentLife -= damage;
            if (_currentLife <= 0) Kill();
            UpdateUI();
            OnDamage?.Invoke(this);
        }

        public void Damage(float damage, Vector3 dir)
        {
            Damage(damage);
        }

        private void UpdateUI()
        {
            if (uiUpdaters != null)
            {
                uiUpdaters.ForEach(i => i.UpdateValue((float) _currentLife/startLife));
            }
        }
    }    
}
