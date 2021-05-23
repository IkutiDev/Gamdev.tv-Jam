using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTwo : MonoBehaviour
{
    Rigidbody characterRigidbody;

    //Movement for going in 4 directions, no jumping
    // Start is called before the first frame update
    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
