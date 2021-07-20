using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSensor : Sensor{

    private Vector3 lastSensorPosition;
    void OnTriggerEnter2D(Collider2D other){
        IsActive = other.CompareTag("Floor");
        if(other.CompareTag("Object"))
            StartCoroutine(nameof(CheckBlockedByObject));
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Floor") || other.CompareTag("Object")){
            IsActive = false;
            lastSensorPosition = Vector3.zero;
            StopCoroutine(nameof(CheckBlockedByObject));
        }
    }
    IEnumerator CheckBlockedByObject(){
        while(!IsActive){
            lastSensorPosition = transform.position;
            yield return new WaitForSecondsRealtime(2);
            if(Mathf.Approximately((int)lastSensorPosition.x, (int)transform.position.x))
                IsActive = true;
        }
    }

}
