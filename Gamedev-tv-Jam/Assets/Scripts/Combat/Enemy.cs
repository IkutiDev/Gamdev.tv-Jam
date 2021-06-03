using Gamedev.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isFlying = false;

    public Stage enemyStage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Projectile>() != null)
        {
            GetComponent<Health>().TakeDamage(other.GetComponent<Projectile>().damage);
        }
    }

}
