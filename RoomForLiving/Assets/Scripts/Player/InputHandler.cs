using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
public class InputHandler : MonoBehaviour
{
    private FurnitureController selectedFurniture;
    private Vector2 startMouseWorldPosition;
    private Vector2 startFurniturePosition;
    private float startFurnitureRotation;

    public enum InputState { Idle, Moving, Rotating}
    public InputState currentState;

    private Camera gameCamera;

    private void Awake() {
        gameCamera = GetComponent<Camera>();
    }

    private void FixedUpdate() {
        switch(currentState) {
            case InputState.Idle:
                if (Input.GetMouseButtonDown(0)) {
                    ActionState(InputState.Moving);
                }
                if(Input.GetMouseButtonDown(1)) {
                    ActionState(InputState.Rotating);
                }
                break;
            case InputState.Moving:
                Vector2 goalPosition = startFurniturePosition + ((Vector2)gameCamera.ScreenToWorldPoint(Input.mousePosition) - startMouseWorldPosition);
                selectedFurniture.GetComponent<Rigidbody2D>().MovePosition(goalPosition);
                break;
            case InputState.Rotating:
                Vector2 a = startMouseWorldPosition - startFurniturePosition;
                Vector2 b = (Vector2)gameCamera.ScreenToWorldPoint(Input.mousePosition) - startFurniturePosition;

                float ang = Vector2.Angle(a, b);

                Vector3 cross = Vector3.Cross(a, b);

                if (cross.z > 0)
                    ang = 360 - ang;
                
                selectedFurniture.GetComponent<Rigidbody2D>().MoveRotation(startFurnitureRotation-ang);
                break;
            default:
                return;
        }
        
        if(!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) {
            currentState = InputState.Idle;
        }
    }

    private void ActionState(InputState nextState) {
        selectedFurniture = GetFurnitureUnderMouse();
        if (selectedFurniture != null) {
            startMouseWorldPosition = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            startFurniturePosition = selectedFurniture.transform.position;
            startFurnitureRotation = selectedFurniture.transform.localEulerAngles.z;
            currentState = nextState;
        }
    }

    private FurnitureController GetFurnitureUnderMouse() {
        if(EventSystem.current.IsPointerOverGameObject()) {
            return null;
        }
        foreach(RaycastHit2D hit in Physics2D.RaycastAll(gameCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero)) {
            FurnitureController controller = hit.collider.GetComponentInParent<FurnitureController>();
            if(controller != null) {
                return controller;
            }
        }
        return null;
    }
}
