using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [Foldout("ProgrammerStuff")]
    [SerializeField] private Transform groundCheckTransform;
    [Foldout("ProgrammerStuff")]
    [SerializeField] private LayerMask groundLayer;
    BoxCollider characterCollider;
    Rigidbody characterRigidbody;
    [SerializeField] GameObject model;
   // SpriteRenderer spriteRenderer;
    Animator animator;
    float input;
    bool isGrounded = true;
    bool isFalling = false;
    bool jump = false;
    bool crouch = false;
    Vector3 standardSize;
    //Movement for left and right + jumping
    // Start is called before the first frame update
    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        characterCollider = GetComponent<BoxCollider>();
       // spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        standardSize = characterCollider.size;
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        if (input != 0)
        {
            if (input < 0) model.transform.localRotation = Quaternion.Euler(0, 180f, 0);
            else model.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //    spriteRenderer.flipX = input < 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
        }
        if (Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            crouch = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouch = false;
        }
        animator.SetBool("crouch",crouch);
        

    }
    private void FixedUpdate()
    {
        isGrounded = GroundCheck();
        if (isGrounded) isFalling = false;
        if (jump)
        {
            jump = false;
            if (isGrounded)
            {
                animator.SetTrigger("jump");
                characterRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                isFalling = true;
            }
        }
        if (crouch)
        {
            if (isGrounded)
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

        if(!crouch) characterRigidbody.AddRelativeForce(0, 0, Time.fixedDeltaTime * input * speed, ForceMode.Impulse);
        animator.SetFloat("currentSpeed", characterRigidbody.velocity.magnitude);
        animator.SetBool("isFalling", isFalling);
        characterRigidbody.velocity = new Vector3(0, characterRigidbody.velocity.y, 0);
    }

    private bool GroundCheck()
    {
        return Physics.Raycast(groundCheckTransform.position,Vector3.down,0.1f,groundLayer);
    }
}
