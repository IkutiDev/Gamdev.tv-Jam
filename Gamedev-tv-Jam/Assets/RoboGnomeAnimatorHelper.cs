using Gamedev.Pickups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboGnomeAnimatorHelper : MonoBehaviour
{
    [SerializeField] private Pickup coin;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform fistTransform;
    [SerializeField] private GameObject modelParent;
    private void ThrowRock()
    {
        var projectile = Instantiate(projectilePrefab, fistTransform.position, projectilePrefab.transform.rotation);
        projectile.GetComponent<EnemyProjectile>().goLeft = modelParent.transform.localRotation.eulerAngles.y >= 180f;
    }
    private void DropCoin()
    {
        Instantiate(coin,transform.position + Vector3.up, coin.transform.rotation);
    }
}
