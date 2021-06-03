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
            if (Input.GetAxisRaw("Jump")>0 && movement.GroundCheck())
            {
                movement.jump = true;
            }
            if (Input.GetAxisRaw("Crouch")>0 && movement.GroundCheck())
            {
                movement.crouch = true;
            }
            if (movement.crouch&&Input.GetAxisRaw("Crouch")==0)
            {
                movement.crouch = false;
            }
            if (movement.GroundCheck())
            {
                if (movement.crouch) return;
                //Dont do attacks if you press mid crouch/jump and after you finish them attack fires
                if (Input.GetAxisRaw("Attack")>0)
                {
                    playerFighter.BasicAttack();
                }
                if (Input.GetAxisRaw("Range Attack") > 0)
                {
                    playerFighter.RangeAttack();
                }
                if (Input.GetAxisRaw("Special Attack") > 0)
                {
                    playerFighter.SpecialAttack();
                }
            }
            else
            {
                if (movement.crouch) return;
                if (Input.GetAxisRaw("Attack") > 0)
                {
                    playerFighter.JumpAttack();
                }
            }
        }
    }
}