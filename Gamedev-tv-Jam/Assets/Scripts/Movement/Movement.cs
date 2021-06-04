using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Gamedev.Movement
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        #region ProgrammerStuff
        [Foldout("ProgrammerStuff")]
        [SerializeField] private Transform groundCheckTransform;
        
        [Foldout("ProgrammerStuff")]
        [SerializeField] private LayerMask groundLayer;
        [Foldout("ProgrammerStuff")]
        [SerializeField] GameObject model;
        #endregion
        BoxCollider characterCollider;
        Rigidbody characterRigidbody;
        Animator animator;
        [HideInInspector] public float input;
        public bool jump = false;
        public bool crouch = false;
        bool isFalling = false;
        Vector3 standardSize;
        AudioSource audioSource;
        [SerializeField] private AudioClip jumpSound;
        //Movement for left and right + jumping
        // Start is called before the first frame update
        void Start()
        {
            characterRigidbody = GetComponent<Rigidbody>();
            characterCollider = GetComponent<BoxCollider>();
            animator = GetComponentInChildren<Animator>();
            standardSize = characterCollider.size;
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            animator.SetBool("crouch", crouch);
            animator.SetFloat("currentSpeed", characterRigidbody.velocity.magnitude);
            animator.SetBool("isFalling", isFalling);
            if (jump)
            {
                if (GroundCheck())
                {
                    audioSource.PlayOneShot(jumpSound);
                    animator.SetTrigger("jump");
                }
            }
        }
        private void FixedUpdate()
        {
            if (input != 0 && !IsInFightingAnimation())
            {
                if (input < 0) model.transform.localRotation = Quaternion.Euler(0, 180f, 0);
                else model.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (GroundCheck()) isFalling = false;
            if (jump)
            {
                jump = false;
                if (GroundCheck())
                {
                    characterRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                    isFalling = true;
                }
            }
            if (crouch)
            {
                if (GroundCheck())
                {
                    model.transform.localPosition = new Vector3(0, 0, 0);
                    characterCollider.size = new Vector3(1, 0.6f, 1);
                }
            }
            else
            {
                model.transform.localPosition = new Vector3(0, -0.5f, 0);
                characterCollider.size = standardSize;
            }

            if (!crouch && !IsInFightingAnimation()) characterRigidbody.AddRelativeForce(0, 0, Time.fixedDeltaTime * input * speed, ForceMode.Impulse);

            characterRigidbody.velocity = new Vector3(0, characterRigidbody.velocity.y, 0);
        }
        private bool IsInFightingAnimation()
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("Punch 1")|| animator.GetCurrentAnimatorStateInfo(0).IsName("Punch 2")|| animator.GetCurrentAnimatorStateInfo(0).IsName("Punch 3") || animator.GetCurrentAnimatorStateInfo(0).IsName("Range Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("Special Attack");
        }
        public bool GroundCheck()
        {
            return Physics.Raycast(groundCheckTransform.position, Vector3.down, 0.1f, groundLayer);
        }
    }
}