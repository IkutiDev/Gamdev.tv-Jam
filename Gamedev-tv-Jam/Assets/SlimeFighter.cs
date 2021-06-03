using Gamedev.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFighter : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponentInParent<Health>().IsDead()) return;
        if (other.tag == "Player")
        {
            GetComponentInChildren<Animator>().SetTrigger("attack");
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
