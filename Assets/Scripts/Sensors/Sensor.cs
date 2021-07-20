using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Sensor : MonoBehaviour{
    private bool _isActive;
    public bool IsActive{
        get{ return _isActive; }
        protected set{ _isActive = value;}
    }

    private Collider2D sensorCollider;
    void Start(){
        sensorCollider = GetComponent<Collider2D>();
        sensorCollider.isTrigger = true;
    }

}
