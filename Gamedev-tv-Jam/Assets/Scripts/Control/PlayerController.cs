using Gamedev.Combat;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamedev.Control
{
    public class PlayerController : MonoBehaviour
    {
        Movement.Movement movement;
        PlayerFighter playerFighter;
        // Start is called before the first frame update
        void Start()
        {
            movement = GetComponent<Movement.Movement>();
            playerFighter = GetComponent<PlayerFighter>();
        }

        // Update is called once per frame
        void Update()
        {
            movement.input = Input.GetAxisRaw("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space) && movement.GroundCheck())
            {
                movement.jump = true;
            }
            if (Input.GetKey(KeyCode.LeftControl) && movement.GroundCheck())
            {
                movement.crouch = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                movement.crouch = false;
            }
            if (movement.GroundCheck())
            {
                if (movement.crouch) return;
                //Dont do attacks if you press mid crouch/jump and after you finish them attack fires
                if (Input.GetMouseButtonDown(0))
                {
                    playerFighter.BasicAttack();
                }
                if (Input.GetMouseButtonDown(1))
                {
                    playerFighter.RangeAttack();
                }
                if (Input.GetMouseButtonDown(2))
                {
                    playerFighter.SpecialAttack();
                }
            }
            else
            {
                if (movement.crouch) return;
                if (Input.GetMouseButtonDown(0))
                {
                    playerFighter.JumpAttack();
                }
            }
        }
    }
}