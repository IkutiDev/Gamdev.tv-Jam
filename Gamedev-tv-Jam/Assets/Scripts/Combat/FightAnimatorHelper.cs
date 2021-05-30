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
        private void Punch_1_Impact()
        {
            playerFighter.Punch_1_Impact();
        }
        private void Punch_1_Finish()
        {
            playerFighter.Punch_1_Finish();
        }
        private void RangeAttackAnimTrigger()
        {
            playerFighter.RangeAttackAnimTrigger();
        }
    }
}