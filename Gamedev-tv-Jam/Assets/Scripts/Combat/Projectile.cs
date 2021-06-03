using Gamedev.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    [SerializeField] private float speed;
    public bool goLeft;
    // Update is called once per frame
    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    void Update()
    {
        if (goLeft) {
            transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>())
        {
            if (other.tag == "Player") return;
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
