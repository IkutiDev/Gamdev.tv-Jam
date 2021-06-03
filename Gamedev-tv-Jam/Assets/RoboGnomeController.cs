using Gamedev.Combat;
using Gamedev.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboGnomeController : MonoBehaviour
{
    PlayerController player;
    Vector3 randomLocalPosition;
    [SerializeField] private float speed;
    [SerializeField] private float damping;

    bool wait = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        int random = Random.Range(0, 18);
        randomLocalPosition = new Vector3(0, 0, random);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Health>().IsDead()) return;
        if (wait)
        {
            if (GetComponentInChildren<Animator>() != null)
            {
                GetComponentInChildren<Animator>().SetFloat("currentSpeed", 0);
            }
            return;
        }
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, randomLocalPosition, speed * Time.deltaTime);
        if (GetComponentInChildren<Animator>() != null)
        {
            GetComponentInChildren<Animator>().SetFloat("currentSpeed", speed);
        }
        if (Vector3.Distance(transform.localPosition, randomLocalPosition) <= 0.1f)
        {
            Attack();
        }
    }
    private void Attack()
    {
        if (GetComponentInChildren<Animator>() != null)
        {
            GetComponentInChildren<Animator>().SetFloat("currentSpeed", 0);
        }
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
        GetComponentInChildren<Animator>().SetTrigger("attack");
        wait = true;
        int random = Random.Range(0, 18);
        randomLocalPosition = new Vector3(0, 0, random);
        Invoke("StopWaiting", 3f);
    }
    private void StopWaiting()
    {
        wait = false;
    }
}
