using Cinemachine;
using Gamedev.Pickups;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Stage : MonoBehaviour
{
    bool firstTrigger = false;
    [SerializeField] private Collider LeftWall;
    [SerializeField] private Collider RightWall;
    [SerializeField] private Pickup[] pickupsToDrop;
    [SerializeField] private Enemy[] enemiesToSpawn;
    [SerializeField] private bool randomizeEnemyOrder;
    [SerializeField] private Transform[] groundSpawners;
    [SerializeField] private Transform[] flyingSpawners;
    [SerializeField] private float enemySpawnerTimeIntervals=5f;
    [SerializeField] private int maxEnemyCount=3;
    [SerializeField] private TMP_Text stageClearedText;

    List<Enemy> enemies;
    List<Pickup> pickups;

    private CinemachineVirtualCamera CVC;
    private Transform alienTransform;
    private int enemiesSpawned=0;
    private int currentEnemyCount=-1;
    private float enemySpawnerTimer=Mathf.Infinity;
    private void Start()
    {
        CVC = FindObjectOfType<CinemachineVirtualCamera>();
        alienTransform = CVC.Follow=CVC.LookAt;
        enemies = new List<Enemy>();
        pickups = new List<Pickup>();
        enemies = enemiesToSpawn.ToList();
        if (pickupsToDrop.Length == 0) return;
        var pickupsCount = Math.Min(enemies.Count, pickupsToDrop.Length);
        for(int i = 0; i < pickupsCount; i++)
        {
            pickups.Add(pickupsToDrop[i]);
        }
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
            StartCoroutine(WaitUntilEnemiesAreDead());
        }
    }
    public void OnEnemyDeath(Vector3 deathPosition,bool shouldDrop, bool shouldCount)
    {
        if (currentEnemyCount == 0)
        {
            Debug.LogError("Cant kill enemy if current count of them is 0, something is wrong!"+currentEnemyCount);
            return;
        }
        if(shouldDrop) DropPickup(deathPosition);
        if(shouldCount)currentEnemyCount--;
    }
    private void DropPickup(Vector3 pickupDropPosition)
    {
        if (pickups.Count == 0) return;
        if(pickups.Count< enemiesToSpawn.Length-enemiesSpawned)
        {
            bool shouldDrop = UnityEngine.Random.value > 0.5f;
            if (!shouldDrop) return;
        }
        var random = UnityEngine.Random.Range(0, pickups.Count);
        var pickup = pickups[random];

        Instantiate(pickup,pickupDropPosition+Vector3.up, pickup.transform.localRotation);
        pickups.RemoveAt(random);
    }
    private void SpawnEnemy()
    {
        Enemy enemyToSpawn;
        Transform spawner;
        if (randomizeEnemyOrder)
        {
            var random = UnityEngine.Random.Range(0, enemies.Count);
            enemyToSpawn = enemies[random];
            spawner = ChooseSpawner(enemyToSpawn);
            enemies.RemoveAt(random);
        }
        else
        {
            enemyToSpawn = enemies[enemiesSpawned];
            spawner = ChooseSpawner(enemyToSpawn);
            //enemies.RemoveAt(enemiesSpawned);
        }
        var spawnedEnemy=Instantiate(enemyToSpawn, spawner.position, spawner.localRotation, spawner);
        spawnedEnemy.enemyStage = this;
        enemiesSpawned++;
        if (spawnedEnemy.shouldCount)
        {
            if (currentEnemyCount == -1) currentEnemyCount = 1;
            else currentEnemyCount++;
        }
        else
        {
            enemySpawnerTimer += 2f;
        }
    }
    private Transform ChooseSpawner(Enemy enemy)
    {
        if (enemy.isFlying)
        {
            return flyingSpawners[UnityEngine.Random.Range(0, flyingSpawners.Length)];
        }
        else
        {
            return groundSpawners[UnityEngine.Random.Range(0, groundSpawners.Length)];
        }
    }
    private void Update()
    {
        if (firstTrigger)
        {
            if (enemiesToSpawn.Length == 0)
            {
                Debug.LogError("You forgot to add enemies to spawn list you dum dum!");
                return;
            }
            if (enemySpawnerTimer > enemySpawnerTimeIntervals)
            {
                enemySpawnerTimer = 0f;
                if (enemiesSpawned >= enemiesToSpawn.Length) return;
                if (currentEnemyCount < maxEnemyCount)
                {
                    SpawnEnemy();
                }
            }
            enemySpawnerTimer += Time.deltaTime;
        }
    }
    private IEnumerator WaitUntilEnemiesAreDead()
    {
        while (enemiesSpawned< enemiesToSpawn.Length)
        {
            yield return null;
        }
        while (currentEnemyCount != 0)
        {
            yield return null;
        }
        EnableCameraAndDisableWalls();
    }
    private void EnableCameraAndDisableWalls()
    {
        stageClearedText.enabled = true;
        Invoke("HideText", 5f);
        CVC.Follow = alienTransform;
        CVC.LookAt = alienTransform;
        LeftWall.isTrigger = true;
        RightWall.isTrigger = true;
    }
    private void HideText()
    {
        stageClearedText.enabled = false;
    }
}
