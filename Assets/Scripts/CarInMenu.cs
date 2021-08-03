using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarInMenu : MonoBehaviour
{
    public string Name;
    public string State;

    [SerializeField] private float _speed;
    private Vector3 _rotation = new Vector3(0, 360, 0);
    
    private void Start()
    {
        transform.DORotate(_rotation, _speed, RotateMode.Fast).SetLoops(-1).SetEase(Ease.Linear).SetRelative();
    }
}
