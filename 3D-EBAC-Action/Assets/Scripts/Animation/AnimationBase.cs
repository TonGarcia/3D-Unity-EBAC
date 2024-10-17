using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Animation
{
    public enum AnimationType
    {
        NONE,
        IDLE,
        RUN,
        ATTACK,
        DEATH
    }
    
    public class AnimationBase : MonoBehaviour
    {
        public Animator animator;
        public List<AnimationSetup> animationSetups;

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            // ToDo: BAD it is creating a loop for each animation change, if a legion of enemies it will crash
            var setup = animationSetups.Find(i => i.AnimationType == animationType).trigger;
            if(setup != null) animator.SetTrigger(setup);
        }
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

    [System.Serializable]
    public class AnimationSetup
    {
        public AnimationType AnimationType;
        public string trigger;
    }
}
