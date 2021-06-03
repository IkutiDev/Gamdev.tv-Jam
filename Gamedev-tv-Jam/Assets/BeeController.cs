using Gamedev.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
