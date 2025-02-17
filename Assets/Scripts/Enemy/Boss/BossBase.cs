// ... existing code ...

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
        }
        #endregion

        #region IDamageable implementations
        public void Damage(float damage)
        {
            OnDamage(damage);
        }
        
        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            // create impact effect based on the given dir of the damage
            transform.DOMove(transform.position - dir, .1f);
        }
        #endregion

// ... existing code ...