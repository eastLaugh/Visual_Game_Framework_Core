using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Assignment;

public class ArrivalPlugin : MonoBehaviour
{
    
    public Arrival arrival;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            arrival.Ticked();
        }
    }
}
