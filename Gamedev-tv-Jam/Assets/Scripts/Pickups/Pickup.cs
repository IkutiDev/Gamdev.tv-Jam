using Gamedev.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PickupType
{
    Coin,
    Health,
    Energy
}
namespace Gamedev.Pickups
{
    [RequireComponent(typeof(MeshCollider))]

    public class Pickup : MonoBehaviour
    {
        [SerializeField] private PickupType pickupType;
        [SerializeField] private int amount;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                switch (pickupType)
                {
                    case PickupType.Coin:
                        FindObjectOfType<CoinCounterUI>().IncreaseCointCount();
                        break;
                    case PickupType.Health:
                        other.GetComponent<Health>().Heal(amount);
                        break;
                    case PickupType.Energy:
                        other.GetComponent<Energy>().IncreaseEnergy(amount);
                        break;
                }
                Destroy(gameObject);
            }
        }
    }
}