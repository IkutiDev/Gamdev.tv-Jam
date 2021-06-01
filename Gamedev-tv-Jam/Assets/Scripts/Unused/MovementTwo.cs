using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTwo : MonoBehaviour
{
    [SerializeField] private float speed;
    Rigidbody characterRigidbody;
    SpriteRenderer spriteRenderer;
    Vector2 input;

    //Movement for going in 4 directions, no jumping
    // Start is called before the first frame update
    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.x != 0)
        {
            spriteRenderer.flipX = input.x < 0;
        }
    }
    private void FixedUpdate()
    {
        characterRigidbody.AddRelativeForce(Time.fixedDeltaTime * input.x * speed, 0, Time.fixedDeltaTime * input.y * speed, ForceMode.Impulse);
        characterRigidbody.velocity = new Vector3(0, characterRigidbody.velocity.y, 0);
    }
}
