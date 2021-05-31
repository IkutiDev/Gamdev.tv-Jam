using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamedev.Combat
{
    public class FightAnimatorHelper : MonoBehaviour
    {
        PlayerFighter playerFighter;
        private void Start()
        {
            playerFighter = GetComponentInParent<PlayerFighter>();
        }
        private void Punch1AnimTrigger()
        {
            playerFighter.Punch_Impact(1);
        }
        private void Punch2AnimTrigger()
        {
            playerFighter.Punch_Impact(2);
        }
        private void Punch3AnimTrigger()
        {
            playerFighter.Punch_Impact(3);
        }
        private void Punch1AnimTriggerFinish()
        {
            playerFighter.Punch_Finish(1);
        }
        private void Punch2AnimTriggerFinish()
        {
            playerFighter.Punch_Finish(2);
        }
        private void Punch3AnimTriggerFinish()
        {
            playerFighter.Punch_Finish(3);
        }
        private void RangeAttackAnimTrigger()
        {
            playerFighter.RangeAttackAnimTrigger();
        }
        private void SpecialAttackAnimTrigger()
        {

        }
        private void JumpingAttackAnimTrigger()
        {
            playerFighter.Punch_Impact(-1);
        }
        private void JumpingAttackAnimTriggerFinish()
        {
            playerFighter.JumpingAttack_Finish();
        }
    }
}