using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        #region Unity Reference Attributes
        public CharacterController characterController;
        public Animator animator;
        public KeyCode jumpKeyCode = KeyCode.Space;
        #endregion

        #region Speed Modifier Attributes
        public float speed = 1f; // movements speed sensitive control
        public float turnSpeed = 1f; // targeting sensitive control (if greater so faster, if lower it means slower)
        public float gravity = 9.8f;
        private float _vSpeed = 0f;
        private static readonly int Run = Animator.StringToHash("Run");
        public float jumpForceSpeed = 15f;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
        
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

            // activate the animations by changing the Parameter values
            animator.SetBool("Run", inputAxisVertical != 0);

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
            characterController.Move(speedVector * Time.deltaTime);
        }
    }
}
