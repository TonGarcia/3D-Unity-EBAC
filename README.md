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

### Challenge Module 28~29

Cena principal: `Scenes/SCN_Main_3D`
Andar pra frente: pressione W
   --> tem um OnExit para parar de andar
Parar/Idle: default, basta não fazer nada (ou pressionar S se travar fora do idle)
Pular (module29): pressione BarraDeEspaço

*Controlados por StateMachine


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
