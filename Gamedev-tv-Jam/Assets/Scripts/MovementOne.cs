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
    float input;
    bool isGrounded = true;
    bool jump = false;
    bool crouch = false;
    //Movement for left and right + jumping
    // Start is called before the first frame update
    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        characterCollider = GetComponent<BoxCollider>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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

    }
    private void FixedUpdate()
    {
        isGrounded = GroundCheck();
        if (jump)
        {
            jump = false;
            if (isGrounded)
            {
                characterRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
        }
        if (crouch)
        {
            if (isGrounded)
            {
                characterCollider.size = new Vector3(1, 0.6f, 1);
            }
        }
        else
        {
            characterCollider.size = new Vector3(1, 1f, 1);
        }
        if(!crouch) characterRigidbody.AddRelativeForce(Time.fixedDeltaTime * input * speed, 0, 0, ForceMode.Impulse);
        characterRigidbody.velocity = new Vector3(0, characterRigidbody.velocity.y, 0);
    }

    private bool GroundCheck()
    {
        return Physics.Raycast(groundCheckTransform.position,Vector3.down,0.5f,groundLayer);
    }
}
