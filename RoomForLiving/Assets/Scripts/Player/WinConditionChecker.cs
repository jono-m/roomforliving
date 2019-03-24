using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinConditionChecker : MonoBehaviour
{
    public UnityEvent OnWin;

    private List<FurnitureController> furnitures;

    private void Start() {
        furnitures = new List<FurnitureController>(FindObjectsOfType<FurnitureController>());
    }

    private void Update() {
        if(DidWin()) {
            OnWin.Invoke();
        }
    }

    private bool DidWin() {
        foreach(FurnitureController controller in furnitures) {
            if(!controller.IsFullyInside()) {
                return false;
            }
        }
        return true;
    }
}
