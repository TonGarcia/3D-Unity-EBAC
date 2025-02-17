# Enemy Shooting and Damage System

## Shooting Mechanics
1. **Enemy Shooting Setup**
   - Enemies use the `EnemyShoot` class which inherits from `EnemyBase`
   - Each enemy has a `GunBase` component that handles the shooting logic
   - Shooting is automatically initiated when the enemy is initialized via `gunBase.StartShoot()`

2. **Projectile System**
   - Enemy projectiles are instantiated from the `PFB_ProjectileEnemy` prefab
   - Projectiles have the following properties:
     - Damage amount: 5 (default)
     - Speed: 50 units per second
     - Tags to hit: ["Player"] (only damages player objects)
     - Auto-destroy timer: 1 second

3. **Enemy Targeting**
   - Enemies can be configured to automatically look at the player
   - This is controlled by the `lookAtPlayer` boolean in `EnemyBase`
   - When enabled, the enemy continuously rotates to face the player

## Damage System
1. **Health Management**
   - Enemies use the `EnemyBase` class which implements `IDamageable`
   - Default starting life: 10 HP
   - Current life is tracked privately

2. **Damage Response**
   - When damaged, enemies:
     - Flash a color effect (if `FlashColor` component is present)
     - Emit particles (15 particles per hit, if `ParticleSystem` is present)
     - Get pushed back in the direction of the hit
     - Play death animation if health reaches 0

3. **Death Handling**
   - On death:
     - Collider is disabled to prevent further interactions
     - Death animation is triggered
     - GameObject is destroyed after 3 seconds
     - Particle effects and visual feedback are played

4. **Visual Feedback**
   - Flash color effect on damage
   - Particle system emission on hits
   - Knockback effect when hit (using DOTween)
   - Death animation transition
