using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementOne : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;
    BoxCollider characterCollider;
    Rigidbody characterRigidbody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    float input;
    bool isGrounded = true;
    bool isFalling = false;
    bool jump = false;
    bool crouch = false;
    //Movement for left and right + jumping
    // Start is called before the first frame update
    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        characterCollider = GetComponent<BoxCollider>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        if (input != 0)
        {
            spriteRenderer.flipX = input < 0;
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
                spriteRenderer.transform.localPosition = new Vector3(spriteRenderer.transform.localPosition.x,0.28f, spriteRenderer.transform.localPosition.z);
                characterCollider.size = new Vector3(1, 0.6f, 1);
            }
        }
        else
        {
            spriteRenderer.transform.localPosition = new Vector3(spriteRenderer.transform.localPosition.x, 0.08f, spriteRenderer.transform.localPosition.z);
            characterCollider.size = new Vector3(1, 1f, 1);
        }

        if(!crouch) characterRigidbody.AddRelativeForce(Time.fixedDeltaTime * input * speed, 0, 0, ForceMode.Impulse);
        animator.SetFloat("currentSpeed", characterRigidbody.velocity.magnitude);
        animator.SetBool("isFalling", isFalling);
        characterRigidbody.velocity = new Vector3(0, characterRigidbody.velocity.y, 0);
    }

    private bool GroundCheck()
    {
        return Physics.Raycast(groundCheckTransform.position,Vector3.down,0.1f,groundLayer);
    }
}
