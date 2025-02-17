# Boss Shooting and Damage System

## Shooting Mechanics
1. **Boss Shooting Setup**
   - Bosses use the `BossShoot` class which inherits from `BossBase`
   - Similar to regular enemies, bosses use a `GunBase` component
   - Shooting is automatically started during initialization

2. **State-Based Combat**
   - Boss uses a state machine with the following states:
     - INIT
     - IDLE
     - WALK
     - ATTACK
     - DEATH

3. **Attack Pattern**
   - Boss has specific attack parameters:
     - Number of attacks per sequence: 5
     - Time between attacks: 0.5 seconds
     - Attack animation scale: 1.1x
     - Attack scale duration: 0.1 seconds

## Damage System
1. **Health Management**
   - Bosses use the `HealthBase` component
   - Starting life: 50 HP (significantly more than regular enemies)
   - Health system uses events/callbacks for damage and death

2. **State-Based Damage Response**
   - Boss behavior changes based on current state
   - Damage can trigger state transitions
   - Death state has special handling:
     - Stops all coroutines
     - Scales down to 0.2x size
     - Triggers death animation

3. **Movement and Combat Pattern**
   - Moves between waypoints during combat
   - Alternates between walking and attacking states
   - Can be configured with multiple waypoints for varied movement
   - Movement speed: 5 units per second

4. **Special Features**
   - Start animation with scaling effect
   - State machine ensures proper transition between behaviors
   - Callback system for coordinating attacks and movement
   - Special death handling with animation and scale effects
