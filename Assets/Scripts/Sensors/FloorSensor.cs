using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSensor : Sensor{
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Floor"))
            IsActive = false;
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Floor"))
            IsActive = true;
    }
}
