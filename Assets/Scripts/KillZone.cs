using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Cone":
                _spawner.ChangeConePosition();
                break;
        }

    }
}
