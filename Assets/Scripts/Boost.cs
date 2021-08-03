using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 15, 0)* _rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cone")
        {
            Vector3 _newPos = transform.position;
            _newPos.x = -1.5f;
            if (other.tag == "Cone")
            {
                _newPos.x = 1.5f;
            }
        }
    }
}
