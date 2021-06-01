using Gamedev.Combat;
using Gamedev.Pickups;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamedev.DebugHelpers
{
    public class DebugObject : MonoBehaviour
    {
        [SerializeField] private PlayerFighter Player;
        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void HitPlayer()
        {
            Player.GetComponent<Health>().TakeDamage(10);
        }
        [Button(enabledMode: EButtonEnableMode.Editor)]
        private void MoveAllObjectsToCorrectYAndZ()
        {
            var player = FindObjectOfType<PlayerFighter>();
            var pickups = FindObjectsOfType<Pickup>();
            foreach (var pickup in pickups)
            {
                pickup.transform.position = new Vector3(pickup.transform.position.x, player.transform.position.y, player.transform.position.z);
            }
        }
    }
}