using Gamedev.Combat;
using Gamedev.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    [SerializeField] private float damping;
    [SerializeField] private float speed;
    [SerializeField] private float minDistance=3f;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private int damage;
    [SerializeField] private Transform damageTransform;

    PlayerController player;
    bool wait = false;
    float currentSpeed=0f;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Health>().IsDead()) return;
        if (wait)
        {
            currentSpeed = 0f;
            if (GetComponentInChildren<Animator>() != null)
            {
                GetComponentInChildren<Animator>().SetFloat("currentSpeed", currentSpeed);
            }
            return;
        }
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        if (Vector3.Distance(transform.position, player.transform.position) >= minDistance)
        {

            transform.position += transform.forward * speed * Time.deltaTime;
            currentSpeed = speed;
            if (GetComponentInChildren<Animator>() != null)
            {
                GetComponentInChildren<Animator>().SetFloat("currentSpeed", currentSpeed);
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= maxDistance)
            {
                wait = true;
                Attack();
                //Here Call any function U want Like Shoot at here or something
            }
        }
    }
    private void Attack()
    {
        GetComponentInChildren<Animator>().SetTrigger("attack");
        Invoke("StopWaiting", UnityEngine.Random.Range(2f,5f));
    }
    private void StopWaiting()
    {
        wait = false;
    }
    public void BiteImpact()
    {
        var targets = Physics.SphereCastAll(damageTransform.position, .7f, Vector3.forward);
        foreach (var target in targets)
        {
            if (target.collider == null) continue;
            var health = target.collider.GetComponent<Health>();
            if (health == null) continue;
            if (target.collider.tag != "Player") continue;
            if (health.IsDead()) continue;
            health.TakeDamage(damage);
        }
    }
}
