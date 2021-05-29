using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighter : MonoBehaviour
{
    //[SerializeField] private 
    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }
}
