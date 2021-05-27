using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PickupType
{
    Coin,
    Health,
    Energy
}
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
                    Debug.Log("Picking up coin +1");
                    break;
                case PickupType.Health:
                    other.GetComponent<Health>().Heal(amount);
                    break;
                case PickupType.Energy:
                    Debug.Log("Energy gained "+amount);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
