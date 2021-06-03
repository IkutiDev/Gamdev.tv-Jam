using Gamedev.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboGnomeController : MonoBehaviour
{
    PlayerController player;
    [SerializeField] Vector3 randomPosition;
    [SerializeField] private float speed;
    [SerializeField] private float damping;

    bool wait = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        randomPosition = new Vector3(0, 0, Random.Range(0, 18));
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, randomPosition, speed * Time.deltaTime);
        GetComponentInChildren<Animator>().SetFloat("currentSpeed", speed);
    }
    private void Attack()
    {
        randomPosition = new Vector3(Random.Range(0, 18), 0, 0);
        wait = false;
    }
}
