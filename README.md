# EBAC Repositories

1. [EBAC Unity Modules 0 to 8 - Unity initial](https://github.com/TonGarcia/EBAC-Unity)
2. [EBAC Unity Modules 9 to 19 - Platform 2D](https://github.com/TonGarcia/Platform2D-EBAC-Unity)
3. [EBAC Unity Modules 20 to 26 - HyperCasual Mobile](https://github.com/TonGarcia/HyperCasual)
4. [EBAC Unity Module 27 - Editor PlugIns](https://github.com/TonGarcia/UnityEditorUIPlugins-EBAC)
5. [EBAC Unity 3D Module 28 ~ 40](https://github.com/TonGarcia/3D-Unity-EBAC)

*Simple Character controller example on Module 29.2

# Unity 3D

## Game Requirements

1. At least 1 player character including integrated animations: walking, jump, die, attack;
2. coins and/or collectable items on the level;
3. trunk and others destroyable items that drops collectables;
4. diversified enemies behaviors;
5. at least 1 simple boss.

## State Machine - GameManager

1. States:
   1. State Menu (UI before GamePlay)
   2. State Game (GamePlay)
      1. methods/sub-state
         1. State OnEnter (just once)
            1. Activate GameObjects
            2. Zero/reset GameManager counters
            3. Animate the initial idle state for each GameObject
            4. Load save 
         2. State OnUpdate (loop just like Unity default Update)
            1. The running Game method 
         3. State OnExit
            1. pause all controls
            2. kill all still alive enemies
            3. check WinCondition
            4. prepare and save achievements
            5. move to next state
   3. State Lose > State Try Again > State Game
   4. State Win > BackMenu

The StateMachine will control the GameMode, like Lara Croft when on ground the animations and actions are different to when she is climbing a wall or holding a string. She must not be able to open the bag while on a string, but the camera and other controls will be enabled. 

## Mecanim (Module 29 - animation)

1. Asset: Assets / Art / 3D Astronaut / Model / MDL_Astronaut
2. Rig: is how the Mesh works on the Skeleton(Rig) while running animations -> rigging = add bones and edges
3. **IMOPRTANT**: the animation RIG_TYPE and the model RIG_TYPE must be the same, example value/type: Generic
4. Animation Preview:
   1. left mouse button = move the camera just forward and backward
   2. center/scroll mouse button = move the camera just forward and backward
   3. right mouse butto = move in the camera in perspective
5. MECANIM = ANIMATOR inspector element (if ANIMATION it is the Legacy)
6. `HasExitTime`: on animator means to wait the animation to finish to change to another animation. In case of `run <> idle` it is bad due it looks like a delay
7. `BlendTree`: open Animator and right click > Create State > From New Blend Tree
   1. double click on the created component to open it
   2. select the opened BlendTree and on the Inspector create 2 **Add Motion Field**
   3. --> it creates the animation smooth change
      1. 0.3 ~> it smothly change
      2. 1 ~> it change animation straight (no transaction time)
      3. 0.5 ~> it plays half time one animation and half time another animation

## 3D Character Controller

1. Check the `Player.cs` code. 
   1. Basically the condig vars are:
      ```public float speed = 1f; // movements speed sensitive control
         public float turnSpeed = 1f; // targeting sensitive control (if greater so faster, if lower it means slower)
         public float gravity = 9.8f;
         private float _vSpeed = 0f;```
   2. The way they modify the behavior:
      1. Rotate based on the amount of pressed button: `transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);`
      2. Moving based on amount of pressed button: `var inputAxisVertical = Input.GetAxis("Vertical");`
      3. Applying the pressed amount to calculate speedVector: `var speedVector = transform.forward * inputAxisVertical * speed;`
      4. Applying gravity to modify how does it work: `_vSpeed -= gravity * Time.deltaTime;`
      5. moving the character: `characterController.Move(speedVector * Time.deltaTime);`
2. Add the Player script to the GameObject Player
   1. for testing/prototyping add a cube as child GameObject Mesh instead of start using the stylized character
   2. Add CharacterController (unity built-in native component)
   3. Set (drag and drop) the component on the Player CharacterController attribute
   4. GRAVITY: if set to -9.8 the gravity will be negative (it is pushing the object against the floor)
4. ANIMATOR:
   1. add the Run animation to the Animator
   2. create a Boolean Run parameter
   3. create a transition from Idle to Run and another transition moving back from Run to Idle
   4. as there is a parameter trigger it is not necessary to set HasExitTime to be checked (so uncheck it)
5. **`Cinemachine`**:
   1. Add/Import: Package Manager > Unity Registry > Cinemachine > install/update
      1. after installation it should be at `Menu > Cinemachine` (2020) or `Menu > GameObject > Cinemachine` (2022)
   2. For this kind of Game create a Virtual Camera. Type of Cameras:
      1. **Cinemachine Virtual Camera**: A general-purpose camera that smoothly transitions between setups and behaviors. Example: Used to follow a player character in a 3D platformer or open-world game.
      2. **Cinemachine FreeLook Camera**: A third-person camera allowing orbiting control around the player. Example: Used in action-adventure games like Assassin's Creed or The Legend of Zelda, where players control the camera around the character.
      3. **Cinemachine 2D Camera**: Optimized for 2D games, keeping the camera locked to a specific plane. Example: Ideal for side-scrolling games like Celeste or Hollow Knight.
      4. **Cinemachine ClearShot Camera**: Automatically picks the best camera angle based on visibility of the target. Example: Used in boss fights or cinematic sequences to choose the most dramatic viewpoint dynamically, often used in **`cutscenes`** or action-packed sequences.
      5. **Cinemachine State-Driven Camera**: Switches between cameras based on game states or animations. Example: Perfect for games with complex animations, such as a fighting game where the camera changes based on attack or special move states.
   3. Configuring it:
      1. create a virtual camera
      2. check the option: `Game Window Guides` (it displays how the camera was configured before the play mode)
      3. `Save During Play`: check it as well, it save what was changed (on the camera config) on play mode
      4. `Follow`: which object it will follow = `Player` (drag&drop)
      5. `LookAt`: which object it will follow = `Player` (drag&drop)
      6. *Check configs on the PDF: `./support-docs/Unity_M29_suporttmaterial_animacoes.pdf` page: 17+

## Creating Guns/Weapons

1. The **Gun standalone Scripts**:
   1. `GunBase`: it is the configuration for the Gun concept
   2. `Projectile`: it is the configuration for the object that is spwaned and shot by the Gun
   3. `CombatSystem`: it is the folder, so `Gun` is a inner folder due it is an example of **CombatSystem**
2. Hierarchy GameObjects:
   1. **Cube** (renamed to Gun) = where/what is the Gun Mesh/location -> it receives the `GunBaseScript` component
      1. **Empty GameObject** = child GameObject, this must be dragged and dropped on the `ShootSpawn`/`GunLocation`
   2. **Projectile** GameObject receives `ProjectileBase` Script
      1. **Sphere**: GameObject as Projectile GameObject child and it will be the "projectile" mesh
   3. *Delete the **PFB_Projectile** from the Hierarchy to Drag&Drop it into the GunBase Script attribute (at **Gun** game object)
   4. *Move the Gun gameobject into the player game object, reset it position and move to the play it is the player body place
   5. *Take a look on the shoot if it is rotating accordingly with the player rotation (it could be a object inside Player>PFB_Astronaut so when Player rotate it would follow, but on the script work as well)
3. Adding the **Gun to the Player** (check the script `PlayerAbilityBase.cs`):
   1. The base create methods that must be overrided
   2. **IMPORTANT:** check the `OnValidate` and it *race condition issue*
   3. **NEW INPUT UNITY SYSTEM**: instead of using `Update()` KeyCode Down, Up... let's use:
      1. Package Manager > Unity Registry > search by "Input System" > Install
      2. IF error about "You are trying to read Input...": Build Settings > Player Settings > Player > Active Input Handling > select "Both"
      3. Instead of `KeyCode` we will replace it by `InputAction` : check the script `PlayerAbilityShoot.cs`
         1. Add the Script as a Component of the Player GameObject
         2. On the component click on the `+` button and select `Add Bidding`
         3. **Double click** on the created bidding item
            1. `Path` attribute:
               1. click on "`Listen`" so when click on the desired Key it will be displayed to be bidded
                  1. **PS.:** in some resolution there is an error and the "Listen" button is behind the option list
            2. `Interactions` attribute:
               1. click on `+` button
               2. **PS.:** it show a list of which interactions this biddind will be trigging, like the Bidding `X` **key** when `Press` do something, when `Hold`this **key** does something else
         4. Check `PlayerAbilityShoot.cs` script, but the **IMPORTANT** thing is instead of `Update` it will be on the `Init()` override
            1. To add an event to be handled it is appending functions to the "performed" in this code: `shootAction.performed += ctx => Shoot();` means:
               1. `shootAction` InputAction object when `performed` it will call the method on the `ctx` (context as param) to the method `Shoot`
            2. **PS.:** as Unity has Active/Enable and Disable GameObjects to show/hide objects, so the **UnityEvents** `OnEnable` and `OnDisable` avoid issues like when you press a key it fire the action when object is not enabled. If the player become a car so the actions outside car must be disabled, as example. The enable is necessary to be able to call the callback (appended event method) as well
            3. The opposite of `performed` is the `canceled` so like pressing `spacebar` to fly, when unpressed `spacebar` the `.cancelled` callback will be called stop flying, as example.
   4. **BEST ORGANIZATION FOR NEW INPUT SYSTEM**:
      1. Create a new folder `Inputs` outside scripts
      2. Create a new Asset (right click on the folder > Create) of the type Input Actions
      3. Select the file on the project, the Inspector will update and click on the inspector the button "Edit Asset"
         1. It will open a Window "Inputs (Input Actions)"
         2. Action Maps = Game Maps (it could be by level, by scene...)
            1. Create a `GamePlay` ActionMap
            2. Rename (by double click) the created action to "Shoot"
            3. Click the colapse button
            4. select the "no bidding" element
            5. Do the same **bidding** the `X` on `path` and set the `interaction`
            6. Close the window and save
         3. Check the checkbox "Generate C# Class" and click on the button "Apply" 
            1. **IMPORATNT**: it generates the script including all the necessary changes including the Enable and Disable as above and so on, so we do not need to control it on our classes
      4. The generated Inputs script will be configured on the Base as it will be inherited
      5. On the `PlayerAbilityBase.cs` create a attr `Inputs` which means the inputs created previously with the InputMap
      6. **IMPORTANT:** the NEW INPUT SYSTEM replace the Update KeyCode checking to trigger and the KeyCode change is made on Inputs file
   5. Script about `GunShootLimits.cs`:
      1. It inherited from `GunBase.cs`
      2. It must overrid the coroutine that shoots to apply the limits before action
      3. AVOID WHILE TRUE, IF UNITY CLOSE OR CRASH IT IS DUE WHILE TRUE AND HIERARCHY CHANGES WILL BE LOST
      4. Check the control to avoid crash on GunShootLimit script
   6. Script about different gun (`GunShootAngle.cs`):
      1. **Check** the `PlayerAbilityShoot` to see how to rotate the guns automatically using currentGun
   7. Integrating UI with the Bullets Limits:
      1. Add an Canva UI Image 
      2. Position it and change anchor to be responsive
      3. Change Image Type to "Filled" so Unity will treat it as "loading" (if circle it would look like a pizza)
      4. Setup images to GameObject that was not there before: `UIGunUpdaters = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();`, it is like jQuery Selector, it is a bad practice due finding & loading complexity on hierarchy
      5. **IMPORTANT**: if the filling image **wrong filling direction**: add `1 - math_calc` --> `uiImage.fillAmount = 1 - (current / max);`
      6. **Reload Animated FILL IMAGE**: `UIGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));` check it on `GunShootLimit.cs` coroutine `IEnumerator RechargeCoroutine()`
      7. Adding Canvas on the GamePlay (instead of UI Canvas):
         1. create or duplicate as new canvas
         2. remove canvas camera
         3. change canvas `RenderMode` to `World Space`
         4. place the canvas where want
         5. *in this project it will be a canvas on player back it head

## Enemies (base - anim + take dmg)

1. Using events to trigger animations and state machine changes;
2. Creating **SerializeField** to debug the current value on the EDITOR (it must be moved back to private to avoid errors);
3. Created Legacy inut to debug on the Update method;
4. **OnDamage** is implemented on the enemy(attacked), not on the player(attacker);
5. --> Kill is not ToKill it is the enemy Death (fix on projects instead of on EBAC just to keep it suitable);
6. **Spawn Animation**:
   1. *It is using Born animation it should be named as Spawn animation as "Start" is already used in many wrong cases
   2. There is a bool control var which could be used in case of changing the game processing levels like Low Resolution, High... to get **Performance** vs **Realism**
7. Add a **BoxCollider** component to the enemy GameObject
8. **ANIMATION**:
   1. Select the PFB_Enemy GameObject > On the Inspector find the Animator > check the file referenced on the `Controller` attr of `Animator` component
   2. Right click on the Animation and left click on `MAKE TRANSITION` > click on next Animation and it will create a transition arrow, click on the transition arrow and set the Trigger that will perform this change (Any State to Idle by Trigger Idle)... CREATE THE TRIGGERS to get it visible as options
   3. *To check it working: Open `Animator` editor tab and check the desired animation
   4. The **Scripts** animations Management will be implemented on the `Scripts/Animation/..cs`
   5. --> bad smell on the animation change using loop
   6. The `EnemyBase` method `OnKill` is waiting 3 seconds to destroy the object and while waiting it trigger the die animation:
      1. `Destroy(gameObject, 3f);`
      2. `PlayAnimationByTrigger(AnimationType.DEATH);`
   7. **Avoid loop animation**:
      1. Animator Tab (it can be acessed on `Project` tab or on the inspector `Component`)
      2. Select (**double click**) the `Death` (or other animation) on Animator tab > uncheck `Loop Time` and `Loop Pose`
9. **COLLISION**:
   1.  IDamageable: C# interface to scale/grow the project including same strategy methods/actions
   2.  **Interface IMPORTANT USAGE**: instead of projectile testing collision with enemies, the projectile will check if the collided GameObject implements IDamageable
       1.  retrieving damageable from collision: `var damageable = otherCollision.transform.GetComponent<IDamageable>();`
       2.  checking if collision was against a IDamageable: `if(damageable != null) damageable.Damage(damageAmount);`
   3.  To make `Projectile` "collisionable":
       1.  add a RigidBody on this Prefab, rigidbody config:
           1.  **disable** the `gravity`
           2.  **freeze** the constraints (position and rotation)
           3.  IF the projectile not visible it could mean the projectile is colliding with the player and it is destroyed
           4.  **Avoid Double Kill Bug**: remove the Collider when it is Dead (check EnemyBase.cs Collider): `if (collider != null) collider.enabled = false;`
10. **Enemies Particles**:
    1.  Shader FX (flash color):
        1.  Check FX helper that changes the Shader using Tweening `FlashColor.`
        2.  Add it as component on the Enemy that contains the Mesh GameObject (`PFB_MonsterSlime_01`)
        3.  Added the flash as cs attribute on the enemy script, so when hitted the flash method is called
        4.  *To ref drag & drop the GameObject that has the Flash component on the attribute so it will be filled
    2.  Particle System FX:
        1.  on Hierarchy right click > Effects > Particle System
        2.  Renderer
            1.  Renderer Mode = Mesh
            2.  Mesh = Sphere
            3.  *Create a new `Material` file under `Art/Materials` folder
                1.  > Material shader type will be `Standard`
                2.  To achive the desired color check the `Albedo` and the `Emission` > `Color` (HDR)
            4.  Drag & Drop the created Material to the Particle Renderer Material
        3.  Emission:
            1.  RATE OVER TIME = 0
            2.  Burst (add item to list)
                1.  gravity 1 (if zero it go up forever), as much gravity fast it falls
        4.  Collision (to be bouncing on the ground) --> set World to be bouncing with anything
            1.  bouncing  = 0 
        5.  size over lifetime:
            1.  check it and on the graph switch to size fall down
        6.  Start Lifetime = 3
        7.  Duration = 3
        8.  Shape
            1.  Shape: Sphere
            2.  Radius: 0.3
        9.  Simulation Space = World (to avoid elements to interact with it)
        10. --> About how to start emitting these particles: on the code check `Enemy.cs` method `OnDamage()`: `if (particleSystem != null) particleSystem.Emit(15);`
        11. **uncheck**: `Play On Awake` and `Looping`
        12. Drag & Drop the particle system on the enemy attr

## Enemies (advanced AI + moves + atacks)

1. **WayPoints** (enemies movements)
   1. Check the created walk script: `EnemyWalk.cs` which inherited from ``EnemyBase`
      1. this first implementation is about waypoints which means to create points on the scene that the NPC will walk to
      2. it goes to a point if the distance is on accepted zone the target location (waypoint) will be the next of the list
      3. **IF** the NPC is lookoing to the wrong location/direction, just rotate the graphic GameObject so it will be looking at the correct target point
      4. the waypoints could be on the PFB character, but the GameDesigner may forget to add the waypoints and crash, so it is better to force GameDesigner to set WayPoints
         1. so created a new empty game object and added the waypoints and the character game object into it new empty game object
2. To feel better the **impact of hitting enemies**:
   1. each shoot that collide with the enemy should push the enemy:
      1. OnDame at EnemyBase script: `transform.position -= transform.forward;` => it just push forward, not check where it came from
      2. To calculate the damage direction check `ProjectileBase.cs` => `damageable.Damage(damageAmount, dir);`
      3. If the enemy go down the ground or move up to the sky check that the `y` must be zero like implemented on `ProjectileBase.cs`
3. **Player collision Flash collor**:
   1. check the updated on Flash.cs (onvalidate runs even on editor runtime)
   2. it can be added by each part of the player body
   3. The `Player` implements the `CharacterController` which use a different collider, so to be able to collide it is necessary to add Box Collider component
   4. Created a EnemyGunBase
   5. To **avoid enemies to kill each other**:
      1. Create Tags
      2. Apply the tags on the GameObjects
      3. On each collision it test the tags
      4. Check the `List<string> tagsToHit` on Projectile to check how it knows if the hit was on the desired target
      5. variant projectile prefab created to be the projectile emited by the enemy and it aims the Player and the player aims the Enemey
   6. **LookAt Player to follow**: 
      1. Enemy looking to the Player: `if(lookAtPlayer) transform.LookAt(_player.transform.position);` check it on the `EnemyBase.cs` script
      2. PS.: remember to check true the "LookAtPlayer" to activate the behaviour
4. Creating **Boss**:
   1. StateMachine with Enum check `Scripts/Enemies/Boss/BossBase.cs`
   2. States on BossStates file with inner classes
   3. `params` keyword means like *params on rails which means that all params will be appended on the var: `OnStateEnter(params object[] objs)`
5. **Boss State Machine**:
   1. File: `BossStates.cs`
   2. Each inner class is a state (walk, idle...) of the GameObject component (enemy, boss, helper...)
   3. Each state goal is to describe how the GameObject will treat on each state case
   4. On the gameobject base, in this case the `BossBase.cs` is where the State Machine are started
   5. There is a `callback` named as `onArrive` to know when an action has done and will be called when it ends, check `BossBase`.`onArrive`
   6. Check the callback `OnArrive()` implementation on `BossStates.cs` and it is passed as `Action onArrive` param
   7. The callback call/invoke is: `onArrive?.Invoke();` , `endCallback?.Invoke();`
   8. The endCallback switch the state that started this new state creating the state machine switching loop
   9. `HEALTH` & `DAMAGE` **Control**:
      1.  `HealthBase` is to manage the Health on a specific script context => uses `CALLBACKS.EVENTS` strategy:
          1.  `healthBase.OnKill += OnBossKill;`
          2.  check `OnDamage?.Invoke(this);` as well
          3.  *Remember to ref `HealthBase` on the `BossBase` component
      2.  `EnemyBase` methods (OnKill, Kill, OnDamage, Damage) => implement the `HARD IMPLEMENTED` strategy directly on the Enemy Script
          


# Challenges

### Challenge Module 28
Cena principal: `Scenes/SCN_Main_3D`
Andar pra frente: pressione W
   --> tem um OnExit para parar de andar
1. ✅ Parar/Idle: default, basta não fazer nada (ou pressionar S se travar fora do idle)
1. ✅ Pular (module29): pressione BarraDeEspaço
*Controlados por StateMachine
Link para a Tag do Módulo: https://github.com/TonGarcia/3D-Unity-EBAC/releases/tag/Module28
Estou enviando também o projeto zipado, pois tivemos problemas com o github em atividades anteriores


### Challenge Module 29
Cena principal: `Scenes/SCN_Main_3D`
1. ✅ Os comandos AWSD estão funcionando com o Character Controller
2. ✅ As animações foram adicionadas e com transição com o BlendTree
3. ✅ Ao pressionar shift o personagem corre
4. ✅ Ao pressionar barra de espaço o personagem pula, caso esteja no chão
Link para a Tag do Módulo: https://github.com/TonGarcia/3D-Unity-EBAC/releases/tag/Module29
Estou enviando também o projeto zipado, pois tivemos problemas com o github em atividades anteriores


### Challenge Module 30
Cena principal: `Scenes/SCN_Main_3D`
1. ✅ Adicionar 2 armas (MachineGun e ShotGun)
2. ✅ MachineGun (✅ Tecla1): o intervalo entre as balas deve ser entre 0.2 segundos
3. ✅ ShotGun (✅ Tecla2): tipo a Angle, só que com delay de 1 segundo


### Challenge Module 31 - Inimigos
1. ✅ ter 2 tipos de inimigos (um verde e um roxo)
   1. ✅ verde
   2. ✅ roxo (mais vida)
2. ✅ os 2 inimigos tem que ter vida
3. ✅ os 2 inimigos tem que ter estado de morte
4. ✅ poder configurar quanto cada inimigo tem de vida
5. ✅ ao ficar sem vida os inimigos devem morrer e ✅ tocar uma animação
6. ✅ ao sofrerem dano tem que emitir as particulas

### Challenge Module 32 - Diferentes Inimigos
1. ⏱️ Ter mais ⏱️ 1 inimigo e ⏱️ 1 chefão (totalizar ✅ 3 inimigos + ⏱️ 1 chefão)
   1. ✅ Inimigo 1 = anda nos checkpoints e é **rosa**
   2. ✅ Inimigo 2 = fica parado em um local de difícil passagem para o player e é **verde**
   3. ✅ Inimigo 3 = fica mirando o player e atirando nele e é **laranja**
   4. ✅ Chefão 1 = ✅ é maior, ✅ tem muita vida (50 enquanto inimigos normais têm 10) ⏱️ atira rajada e é **roxo**
2. ⏱️ chefão: ser maior e com mais vida

# Rider BugFix

To avoid file error while no source code on Rider, mainly when creating CustomEditors:
1. Menu > File > Invalidate Caches... > select everything and let it restart and wait it checker to rerun

*IF ToArray error, remember to add: `using System.Linq;`

# UnityTemplate
1. [Unity GitHub Repo Template](https://github.com/TonGarcia/UnityTemplate)
2. [Unity GitLab GBaaS (Firebase+PlayFab+GBaas) Template Sample](https://gitlab.com/kpihunters/GBaaS/unity-gbaas-template)

*Remember to copy and paste `.gitattributes` & `.gitignore` into it project sub folder.    
**Remember to protect it new repository branchs

# Branches Strategy
1. Develop: used to `programming` updates
2. Art: used to `art experiments like scenes concepts` and others `arts` stuff
3. Each new task should be a fork of it branch type like `develop_mobile_35` or `art_character_34`
4. The workflow should be: `task branch` like `develop_mobile_35` merged into `develop`, once ok the develop should be merged into `main` and `art` and `develop` should be updated from that new stable `main` branch

# Errors HotFix

## IF Unity incompatible version
1. IF on MacOS:
   1. install LFS command globally: `brew install git-lfs`
   2. install git lfs to repo: `git lfs install`
   3. pull LFS files: `git lfs pull`
1. If plastic error:
   1. Unity > Version Control (icon) > LogIn
   2. Unity > Project Settings > Version Control > Unity Version Control Settings > DISABLE

# Rider <> Unity tips
1. Video for more information: https://www.youtube.com/watch?v=O1oOAM-AdbE
2. If Rider did not tagged the project as Unity Project, so it is necessary to open the root project folder on Rider, so Rider will set it up correctly

# Git Large Files

1. Duplicate the .gitignore from root dir to inside the Game Project dir
2. Install: 
   1. download and run the installer
   2. `git lfs install`
3. Add large files extension: 
   1. `git lfs track "*.psd" "*.png" "*.jpg" "*.jpg" "*.gif" "*.mp4" "*.mp3" "*.fbx"`
   2. `git lfs track "**.psd" "**.png" "**.jpg" "**.jpg" "**.gif" "**.mp4" "**.mp3" "**.fbx" "**.dll"`
4. Attributes tracker: `git add .gitattributes`
5. If git not tracking any extension run it: `git lfs migrate import --include="*.extension"`

# Unity Tips
1. Switch graphic manipulation: **QWERTY**
2. Lock Tab: lock Inspector tab to be able to select many Hierarchy elements without switching it Inspector visualization
3. Use Unity DOTs and ECS instead of GameObject instantiations that replicate same C# code at runtime: https://www.youtube.com/watch?v=18f2LeIXGo4

# Unity VR

## Google Cardboard

1. Link to Video: https://www.youtube.com/watch?v=O9NCXV88gPI
2. Remember to import the asset after cloning at the PackageManager
