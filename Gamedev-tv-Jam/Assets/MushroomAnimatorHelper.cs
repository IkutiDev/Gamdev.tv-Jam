using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAnimatorHelper : MonoBehaviour
{
    private void MushroomAttack()
    {
        GetComponentInParent<MushroomController>().BiteImpact();
    } 
    private void Spawned()
    {
        GetComponentInParent<MushroomController>().enabled = true;
    }
}
