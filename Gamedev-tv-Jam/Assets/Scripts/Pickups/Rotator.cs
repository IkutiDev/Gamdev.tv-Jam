using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamedev.Pickups
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private bool zAxis;
        [SerializeField] private bool xAxis;
        [SerializeField] private bool yAxis;

        // Update is called once per frame
        void Update()
        {
            float xDegrees = xAxis ? speed : 0f;
            float zDegrees = zAxis ?speed:0f;
            float yDegrees = yAxis ? speed : 0f;
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x+xDegrees,transform.localRotation.eulerAngles.y+yDegrees,transform.localRotation.eulerAngles.z+zDegrees);
        }
    }
}