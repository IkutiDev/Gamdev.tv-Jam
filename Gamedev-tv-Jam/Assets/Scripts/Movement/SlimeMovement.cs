using Gamedev.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    bool goLeft;
    bool startLeft;
    private void Start()
    {
        startLeft=goLeft = transform.rotation.eulerAngles.y >= 180f;
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Health>().IsDead()) return;
        if (goLeft)
        {
            transform.position = new Vector3(transform.position.x - (speed*Time.deltaTime), transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            goLeft = !goLeft;
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y+180f, 0f);
        }
    }
}
