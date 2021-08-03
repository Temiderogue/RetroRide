using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    [SerializeField] private float _xSpeed;
    [SerializeField] private float _zSpeed;
    private void FixedUpdate()
    {
        transform.Rotate(_xSpeed, 0f, _zSpeed);
    }
}
