using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    bool firstTrigger = false;
    [SerializeField] private Collider LeftWall;
    [SerializeField] private Collider RightWall;

    private CinemachineVirtualCamera CVC;
    private Transform alienTransform;
    private void Start()
    {
        CVC = FindObjectOfType<CinemachineVirtualCamera>();
        alienTransform = CVC.Follow=CVC.LookAt;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (firstTrigger) return;
        if (other.tag == "Player")
        {
            firstTrigger = true;
            Debug.Log("Entered stage");
            CVC.Follow = null;
            CVC.LookAt = null;
            LeftWall.isTrigger = false;
            RightWall.isTrigger = false;
            //Start spawning enemies
            StartCoroutine(SpawnEnemies());
        }
    }
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return null;
        }
        EnableCameraAndDisableWalls();
    }
    private void EnableCameraAndDisableWalls()
    {
        CVC.Follow = alienTransform;
        CVC.LookAt = alienTransform;
        LeftWall.isTrigger = true;
        RightWall.isTrigger = true;
    }
}
